using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace DetalleOrden.Validaciones
{
    class CampoVacio : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string cadena = value as string;
            if (cadena != null)
            {
                if (cadena.Length <= 0)
                    return new ValidationResult(false, "Debes llenar campo");

                return ValidationResult.ValidResult;

            }
            return new ValidationResult(false, "Debe llenar campo");
        }
    }
}
