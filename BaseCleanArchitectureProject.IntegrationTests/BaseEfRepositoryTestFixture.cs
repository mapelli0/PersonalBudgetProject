﻿using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using BaseCleanArchitectureProject.Infrastructure;
using BaseCleanArchitectureProject.Infrastructure.Data;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Salftech.SharedKernel;
using Salftech.SharedKernel.Interfaces;

namespace BaseCleanArchitectureProject.IntegrationTests {

	public class BaseEfRepositoryTestFixture {
		protected BaseCleanArchitectureProjectDbContext _dbContext;
		protected Mock<IMediator> _dispatcherMock;
		protected IMediator _dispatcher;
		protected IContainer _container;


		public BaseEfRepositoryTestFixture() {
			var builder = new ContainerBuilder();
			builder.RegisterModule(new DefaultInfrastructureModule(isDevelopment: true));
			builder.RegisterType<NullLoggerFactory>().As<ILoggerFactory>().SingleInstance();
			builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();
			builder.Register(c => CreateNewContextOptions()).As<DbContextOptions<BaseCleanArchitectureProjectDbContext>>().InstancePerLifetimeScope();
			builder.RegisterType<BaseCleanArchitectureProjectDbContext>().AsSelf().InstancePerLifetimeScope();
			_container = builder.Build();
			_dbContext = _container.Resolve<BaseCleanArchitectureProjectDbContext>();
			
			_dbContext.SeedData(default).GetAwaiter().GetResult();
		}




		protected static DbContextOptions<BaseCleanArchitectureProjectDbContext> CreateNewContextOptions() {
			// Create a fresh service provider, and therefore a fresh
			// InMemory database instance.
			var serviceDBProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

			// Create a new options instance telling the context to use an
			// InMemory database and the new service provider.
			var dbBuilder = new DbContextOptionsBuilder<BaseCleanArchitectureProjectDbContext>();
			dbBuilder.UseInMemoryDatabase("BaseCleanArchitectureProject").UseInternalServiceProvider(serviceDBProvider);
			return dbBuilder.Options;
		}

		protected async Task<TRepository> GetRepositoryWithMocks<TRepository, TEntity>() {
			var options = CreateNewContextOptions();
			_dispatcherMock = new Mock<IMediator>();
			var configuration = new MapperConfiguration(cfg => cfg.AddMaps(new[] {
																						"BaseCleanArchitectureProject"
																				}));
			var mapper = new Mock<IMapper>();
			var validator = MockValidator<TEntity>();
			_dbContext = new BaseCleanArchitectureProjectDbContext(options, _dispatcherMock.Object);
			await _dbContext.SeedData(default);
			object[] args = {
									_dbContext, mapper.Object, validator.Object
							};
			return (TRepository)Activator.CreateInstance(typeof(TRepository), args);
		}

		private static Mock MockValidator<TEntity>() {
			var mockValidator = new Mock<IValidator<TEntity>>();
			var validationResult = new ValidationResult();
			mockValidator.Setup<Task<ValidationResult>>(v => v.ValidateAsync(It.IsAny<TEntity>(), default)).Returns(Task.FromResult(validationResult));
			return mockValidator;
		}

		protected async Task<IRepository<TEntity, Guid>> GetRepository<TEntity>() where TEntity: IBaseEntityId<Guid>, IRoot {
			
			var repository = _container.Resolve<IRepository<TEntity, Guid>>();
			return repository;
		}

	}

}