using SharedKernel.Interfaces;

namespace SharedKernel.Security.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IUser user);
    }
}
