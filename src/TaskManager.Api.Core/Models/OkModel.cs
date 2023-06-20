namespace TaskManager.Api.Core.Models
{
    /// <summary>
    /// Status 200 return model
    /// </summary>
    public class OkModel
    {
        public OkModel(object result)
        {
            Success = true;
            Result = result;
        }

        public bool Success { get; private set; }
        public object Result { get; private set; }
    }
}