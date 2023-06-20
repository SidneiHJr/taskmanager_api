using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Api.Core.Models
{
    /// <summary>
    /// Status 400 return model
    /// </summary>
    public class BadRequestModel
    {
        public BadRequestModel(IEnumerable<object> errors)
        {
            Success = false;
            Errors = errors;
        }

        public bool Success { get; private set; }
        public IEnumerable<object> Errors { get; private set; }
    }
}
