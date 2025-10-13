using RegistroNF.Core.Enum;

namespace RegistroNF.Core.Common
{
    public class CustomException : Exception
    {
        public CustomException(string error, ErrorType errorType)
        {
            Error = error;
            ErrorType = errorType;
        }

        public string Error { get; }
        public ErrorType ErrorType { get; }
    }
}
