using MediatR;

namespace AllInSkateChallenge.Features.Administration.UserDelete
{
    public class AdminDeleteUserCommand : IRequest
    {
        public string UserId { get; set; }
    }
}
