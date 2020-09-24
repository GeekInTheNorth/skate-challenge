using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace AllInSkateChallenge.Features.MileageLogging
{
    public interface IMileageLoggingRepository
    {
        Task SaveAsync(IdentityUser user, MileageLoggingEntryModel mileageUpdate);
    }
}
