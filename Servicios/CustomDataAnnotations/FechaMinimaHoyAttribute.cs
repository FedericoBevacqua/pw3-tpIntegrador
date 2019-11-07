using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.CustomDataAnnotations
{
	public class FechaMinimaHoyAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)

		{

			DateTime CurrentDate = DateTime.Now;

			string Message = string.Empty;

			if (Convert.ToDateTime(value) < CurrentDate)

			{

				Message = "La fecha no puede ser menor al dia de hoy";

				return new ValidationResult(Message);

			}

			return ValidationResult.Success;

		}
	}
}
