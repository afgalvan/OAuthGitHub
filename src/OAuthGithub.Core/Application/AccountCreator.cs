using System.Threading;
using System.Threading.Tasks;
using OAuthGithub.Core.Domain;

namespace OAuthGithub.Core.Application
{
    public class AccountCreator
    {
        private readonly JwtGenerator    _jwtGenerator;
        private readonly Hasher          _hasher;
        private readonly IUserRepository _userRepository;

        public AccountCreator(JwtGenerator jwtGenerator, IUserRepository userRepository,
            Hasher hasher)
        {
            _jwtGenerator   = jwtGenerator;
            _userRepository = userRepository;
            _hasher         = hasher;
        }

        public async Task<string> Create(string username, string email, string password, CancellationToken cancellation)
        {
            User user = new(username, email, password);

            user.Password = _hasher.Hash(user.Password);
            User savedUser = await _userRepository.Save(user, cancellation);
            return _jwtGenerator.Generate(savedUser);
        }
    }
}
