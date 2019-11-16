using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.CustomDataAnnotations
{
	public class MayorDeEdadAttribute : ValidationAttribute
	{
		/*public MayorDeEdadAttribute()
		{
		}
		public override bool IsValid(object value)
		{
			var FechaNacimiento = (DateTime)value;
			if (DateTime.Now.AddYears(-18).CompareTo(FechaNacimiento) <= 0)
			{
				return true;
			}
			return false;
	}*/
		//TODO: Opcion 2
		/*protected override ValidationResult IsValid(object value, ValidationContext validationContext)
	   {
		   value = (DateTime)value;
		   if (DateTime.Now.AddYears(-18).CompareTo(value) <= 0)
		   {
			   return ValidationResult.Success;
		   }
		   else
		   {
			   return new ValidationResult("Debe ser mayor a 18 años");
		   }*/

		//Opcion 3

		private int _EdadMaxima;
		public MayorDeEdadAttribute(int EdadMaxima)
		{
			_EdadMaxima = EdadMaxima;
		}
		public override bool IsValid(object value)
		{
			if (value == null) return true;
			var FechaNacimiento = (DateTime)value;

			double Edad = (DateTime.Now.Subtract(FechaNacimiento)).TotalDays / 365;
			if (Edad < _EdadMaxima){
				return false;
			}
			return true;
		}
	}
}
