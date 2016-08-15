using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLine
{
    interface IValidate
    {
        TypeOfValidation.ValidationName NameOfValidation { get; set; }
        string Details { get; set; }
        bool PassedValidation { get; set; }

        /// <summary> Returns if it Pass/Fails (True/False) the check </summary>
        /// <returns>Boolean</returns>
        bool Check();
    }
}
