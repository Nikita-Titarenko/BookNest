using AutoMapper;
using BookNest.Application.Dtos.AppUsers;
using BookNest.Server.Requests.AppUsers;

namespace BookNest.Server.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<RegisterRequestModel, RegisterDto>();
            CreateMap<LoginRequestModel, LoginDto>();
        }
    }
}
