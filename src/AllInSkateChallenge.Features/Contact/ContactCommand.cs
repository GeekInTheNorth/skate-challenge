
using MediatR;

namespace AllInSkateChallenge.Features.Contact
{
    public class ContactCommand : IRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }
    }
}
