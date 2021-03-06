﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUILibrary.Helper
{
    public class ConfigHelper : IConfigHelper
    {
        //TODO:Move this from config to API
        public decimal GetTaxRate()
        {
            string rateText = ConfigurationManager.AppSettings["taxRate"];
            bool IsValidTaxRate = Decimal.TryParse(rateText, out decimal output);
            if (IsValidTaxRate == false)
            {
                throw new ConfigurationErrorsException("The Tax Rate Is Not set Up Properlry");
            }
            return output;
        }
    }
}
