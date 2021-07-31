using System.Threading;
using System.Threading.Tasks;
using OAuthGitHub.Api.Domain;

namespace OAuthGitHub.Api.Application
{
    public class AuthService
    {
        private readonly JwtGenerator    _jwtGenerator;
        private readonly Hasher       _hasher;
        private readonly IUserRepository _userRepository;

        public AuthService(JwtGenerator jwtGenerator, IUserRepository userRepository,
            Hasher hasher)
        {
            _jwtGenerator   = jwtGenerator;
            _userRepository = userRepository;
            _hasher      = hasher;
        }

        public async Task<string> SignUp(User user, CancellationToken cancellation)
        {
            user.Password = _hasher.Hash(user.Password);
            User savedUser = await _userRepository.Save(user, cancellation);
            return _jwtGenerator.Generate(savedUser);
        }
    }
}
