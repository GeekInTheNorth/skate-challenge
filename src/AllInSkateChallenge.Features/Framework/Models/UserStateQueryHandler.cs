namespace AllInSkateChallenge.Features.Framework.Models
{
    using System.Threading;
    using System.Threading.Tasks;

    using AllInSkateChallenge.Features.Data;

    using MediatR;

    using Microsoft.EntityFrameworkCore;

    public class UserStateQueryHandler : IRequestHandler<UserStateQuery, UserStateQueryResponse>
    {
        private readonly ApplicationDbContext context;

        public UserStateQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<UserStateQueryResponse> Handle(UserStateQuery request, CancellationToken cancellationToken)
        {
            var response = new UserStateQueryResponse();
            if (request?.User != null)
            {
                response.HasStravaImports = await context.StravaEvents.AnyAsync(x => x.ApplicationUserId.Equals(request.User.Id) && !x.Imported, cancellationToken);
            }

            return response;
        }
    }
}
