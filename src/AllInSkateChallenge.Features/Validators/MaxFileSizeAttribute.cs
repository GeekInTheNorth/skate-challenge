namespace AllInSkateChallenge.Features.Validators
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            this.maxFileSize = maxFileSize;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file && file.Length > maxFileSize)
            {
                var errorMessage = ErrorMessage ?? $"Maximum allowed file size is {maxFileSize} bytes.";
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
