using MediatR;

namespace AllInSkateChallenge.Features.Skater.Registration
{
    public class SendRegistrationEmailCommand : IRequest
    {
        public string Email { get; set; }

        public string EmailConfirmationUrl { get; set; }

        public bool FromSkateEverywhere { get; set; }
    }
}
