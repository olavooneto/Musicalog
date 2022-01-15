using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicalog.Domain.Constants
{	public static class AppContants
	{
		public static string ValidatorStringNotEmpty = "{PropertyName} must not be empty";

		public static string ValidatorStringNotNull = "{PropertyName} must not be null";

		public static string ValidatorIntGreaterThen0 = "{PropertyName} must be greater then 0";

		public static string ValidatorIsInEnum = "{PropertyName} must be a invalid value, please check it";
	}
}
