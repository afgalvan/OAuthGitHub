using System.Threading.Tasks;
using Newtonsoft.Json;
using OAuthGithub.Core.Domain;
using OAuthGitHub.Integration.Test.Mocks;
using TechTalk.SpecFlow;

namespace OAuthGitHub.Integration.Test.Steps
{
    [Binding]
    public class DatabaseSteps
    {
        private readonly TestDbContext _databaseContext;

        public DatabaseSteps(TestDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [Given(@"an empty database")]
        public void GivenAEmptyDatabase()
        {
            _databaseContext.Database.EnsureDeleted();
            _databaseContext.Database.EnsureCreated();
        }

        [Given(@"the following user already exists:")]
        public async Task GivenTheFollowingUserAlreadyExists(string userSchema)
        {
            var user = JsonConvert.DeserializeObject<User>(userSchema);
            await _databaseContext.Users.AddAsync(user);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
