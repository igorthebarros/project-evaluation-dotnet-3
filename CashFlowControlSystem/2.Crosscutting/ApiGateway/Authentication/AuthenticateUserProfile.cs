using AutoMapper;
using SharedKernel.Common;

namespace ApiGateway.Authentication
{
    /// <summary>
    /// AutoMapper profile for authentication-related mappings
    /// </summary>
    public sealed class AuthenticateUserProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticateUserProfile"/> class
        /// </summary>
        public AuthenticateUserProfile()
        {
            CreateMap<User, AuthenticateUserResponse>()
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
        }
    }
}
