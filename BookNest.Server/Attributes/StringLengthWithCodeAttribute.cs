using System.ComponentModel.DataAnnotations;

namespace BookNest.Server.Attributes
{
    public class StringLengthWithCodeAttribute : StringLengthAttribute
    {
        public int Code { get; }

        public StringLengthWithCodeAttribute(int maximumLength, int code) : base(maximumLength)
        {
            Code = code;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var baseResult = base.IsValid(value, validationContext);
            if (baseResult == ValidationResult.Success)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(
                $"{Code}:{baseResult?.ErrorMessage ?? "Invalid value"}",
                baseResult?.MemberNames ?? Enumerable.Empty<string>()
            );
        }
    }
}
