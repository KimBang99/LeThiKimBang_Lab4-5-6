﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace lab04_2.ViewModels
{
    public class Time : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                "HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.None, out dateTime);
            return isValid;
        }
    }
}