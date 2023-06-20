using System.Collections.Generic;

namespace TaskManager.Api.Core.Models
{
    /// <summary>
    /// Status 500 return model
    /// </summary>
    public class InternalServerErrorModel
    {
        public InternalServerErrorModel(object result, ICollection<string> errors)
        {
            Result = result;
            Errors = errors;
        }

        public object Result { get; private set; }
        public ICollection<string> Errors { get; private set; }
    }
}