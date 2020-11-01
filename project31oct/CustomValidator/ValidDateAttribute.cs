using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project31oct.CustomValidator
{
    public class ValidDateAttribute:ValidationAttribute
    {
        private readonly DateTime validDate;

        public ValidDateAttribute()
        {
            this.validDate = DateTime.Now;
        }
        public override bool IsValid(object value)
        {
            var date = (DateTime)value; ;
            return date < this.validDate;
        }
    }
}
