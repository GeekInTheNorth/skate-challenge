
using MediatR;

namespace AllInSkateChallenge.Features.Administration.UserUpdate
{
    public class AdminUpdateUserCommand : IRequest
    {
        public string UserId { get; set; }

        public bool? HasPaid { get; set; } 
    }
}
