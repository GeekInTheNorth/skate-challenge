using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AllInSkateChallenge.Features.Contact
{
    public class ContactViewModel : IValidatableObject
    {
        [Display(Name = "Name")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Email Address")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Reason")]
        [StringLength(500)]
        public string Reason { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Message")]
        [StringLength(500)]
        [Required]
        public string Message { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // This is a honeypot and should be empty
            if (!string.IsNullOrWhiteSpace(Reason))
            {
                yield return new ValidationResult("Invalid entry", new[] { nameof(Reason) });
            }
        }

        public ContactCommand ToCommand() => new ContactCommand { Email = Email, Name = Name, Message = Message };
    }
}
