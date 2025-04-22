using SharedKernel.Common;
using SharedKernel.Enums;
using SharedKernel.Interfaces;

namespace SharedKernel.Specifications
{
    public class ActiveUserSpecification : ISpecification<User>
    {
        public bool IsSatisfiedBy(User user)
        {
            return user.Status == UserStatus.Active;
        }
    }
}
