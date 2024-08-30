using CurrencyConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConventer
{
    static class CurrencyUtility
    {
        public static double Convert(double amountToConvert, double currencyRate)
        {
            return amountToConvert * currencyRate;
        }
    }
}
