﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMamagerClassLibrary
{
    public  class ConfigHelper 
    {
        //TODO:Move this from config to API
        public static decimal GetTaxRate()
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
