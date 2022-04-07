using Microsoft.AspNetCore.Mvc.ModelBinding;
using SimpleCRUDExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCRUDExample.Helper
{
    public static class ModelStateExtensions
    {
        public static IEnumerable<ErrorViewModel> AllErrors(this ModelStateDictionary modelState)
        {
            var result = from ms in modelState
                         where ms.Value.Errors.Any()
                         let fieldKey = ms.Key
                         let errors = ms.Value.Errors
                         from error in errors
                         select new ErrorViewModel(fieldKey, error.ErrorMessage);

            return result;
        }
    }
}
