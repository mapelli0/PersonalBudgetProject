using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using AutoMapper;
using BaseCleanArchitectureProject.Core.Events;
using BaseCleanArchitectureProject.Core.Interfaces;
using BaseCleanArchitectureProject.Infrastructure.Data;
using BaseCleanArchitectureProject.Infrastructure.EmailSender;
using FluentValidation;
using MediatR;
using Salftech.SharedKernel.Interfaces;
using Module = Autofac.Module;

namespace BaseCleanArchitectureProject.Infrastructure {

	public class DefaultInfrastructureModule: Module {
		private readonly List<Assembly> _assemblies = new List<Assembly>();
		private readonly bool _isDevelopment;

		public DefaultInfrastructureModule (bool isDevelopment, Assembly callingAssembly = null) {
			_isDevelopment = isDevelopment;
			//var coreAssembly = Assembly.GetAssembly(typeof(DatabasePopulator));
			var infrastructureAssembly = Assembly.GetAssembly(typeof(Repository<,>));
			//_assemblies.Add(coreAssembly);
			_assemblies.Add(infrastructureAssembly);
			if (callingAssembly != null) {
				_assemblies.Add(callingAssembly);
			}
		}

		protected override void Load (ContainerBuilder builder) {
			if (_isDevelopment) {
				RegisterDevelopmentOnlyDependencies(builder);
			} else {
				RegisterProductionOnlyDependencies(builder);
			}
			RegisterCommonDependencies(builder);
		}

		private void RegisterCommonDependencies (ContainerBuilder builder) {
			RegisterMediatr(builder);
			RegisterRepositories(builder);
			RegisterAutomapper(builder);
			RegisterValidator(builder);

			//builder.RegisterType<EmailSender>().As<IEmailSender>()
			//    .InstancePerLifetimeScope();
		}

		private void RegisterValidator (ContainerBuilder builder) {
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			builder.RegisterAssemblyTypes(assemblies).Where(t => typeof(AbstractValidator<>).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic).AsImplementedInterfaces().InstancePerLifetimeScope();

			//builder.RegisterType<FluentValidationModelValidatorProvider>().As<ModelValidatorProvider>();
			builder.RegisterType<AutofacValidatorFactory>().As<IValidatorFactory>().SingleInstance();
		}

		private void RegisterMediatr (ContainerBuilder builder) {
			builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
			builder.Register<ServiceFactory>(context => {
												var c = context.Resolve<IComponentContext>();
												return t => c.Resolve(t);
											});
			builder.RegisterAssemblyTypes(typeof(BaseDomainEvent).GetTypeInfo().Assembly).AsImplementedInterfaces();
		}

		private static void RegisterRepositories (ContainerBuilder builder) {
			builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();
			builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().InstancePerLifetimeScope();
		}

		private void RegisterAutomapper (ContainerBuilder builder) {
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			builder.RegisterAssemblyTypes(assemblies).Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic).As<Profile>();
			builder.Register(c => new MapperConfiguration(cfg => {
															foreach (var profile in c.Resolve<IEnumerable<Profile>>()) cfg.AddProfile(profile);
														}))
					.AsSelf()
					.SingleInstance();
			builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();
		}

		private void RegisterDevelopmentOnlyDependencies (ContainerBuilder builder) {
			builder.RegisterType<LocalEmailSender>().As<IEmailSender>().InstancePerLifetimeScope();
		}

		private void RegisterProductionOnlyDependencies (ContainerBuilder builder) {
			// TODO: Add production only services
		}
	}

}