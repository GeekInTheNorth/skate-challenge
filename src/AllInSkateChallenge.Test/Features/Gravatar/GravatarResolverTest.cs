using AllInSkateChallenge.Features.Gravatar;

using NUnit.Framework;

namespace AllInSkateChallenge.Test.Features.Gravatar
{
    [TestFixture]
    public class GravatarResolverTest
    {
        private IGravatarResolver gravatarResolver;
        
        [SetUp]
        public void SetUP()
        {
            gravatarResolver = new GravatarResolver();
        }

        [Test]
        [TestCase("myemailaddress@example.com", "https://www.gravatar.com/avatar/0bc83cb571cd1c50ba6f3e8a78ef1346")]
        [TestCase("", "https://www.gravatar.com/avatar/00000000000000000000000000000000")]
        [TestCase(null, "https://www.gravatar.com/avatar/00000000000000000000000000000000")]
        public void CorrectlyGeneratesHash(string emailAddress, string expectedValue)
        {
            // Act
            var hash = gravatarResolver.GetGravatarUrl(emailAddress);

            // Assert
            Assert.That(hash, Is.EqualTo(expectedValue));
        }
    }
}
