using CleanArchitecture.Advanced.Common.Constants;

namespace CleanArchitecture.Advanced.Common.Exceptions
{
    [Serializable]
    public class ValidatorException : Exception
    {
        public IEnumerable<string> ValidationErrors { get; }

        public ValidatorException(IEnumerable<string> validationErrors) : base(CommonConstants.GenericValidationMessage)
        {
            ValidationErrors = validationErrors;
        }
    }
}
