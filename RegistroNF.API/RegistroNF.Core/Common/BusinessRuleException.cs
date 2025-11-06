namespace RegistroNF.Core.Common
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException() { }

        public BusinessRuleException(string message) : base(message) { }

        public BusinessRuleException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public BusinessRuleException(string message, Exception innerException) 
            : base(message, innerException) { }

        public BusinessRuleException(string message, string errorCode, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
        
        public string ErrorCode { get; } = string.Empty;
    }
}
