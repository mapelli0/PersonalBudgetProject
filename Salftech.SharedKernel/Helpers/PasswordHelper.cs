using System;
using System.Linq;

namespace Salftech.SharedKernel.Helpers {

	public class PasswordHelper {

		const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		const string digits = "0123456789";

		public static string GenerateRandomPassword (int length) {
			Random random = new Random();
			return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}

}