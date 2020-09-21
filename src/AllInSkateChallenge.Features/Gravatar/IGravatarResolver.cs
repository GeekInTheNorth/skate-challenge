namespace AllInSkateChallenge.Features.Gravatar
{
    public interface IGravatarResolver
    {
        string GetGravatarUrl(string emailAddress);
    }
}
