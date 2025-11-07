using AutoMapper;
using BookNest.Application.Dtos.AppUserRooms;

namespace BookNest.Server.Profiles
{
    public class UserRoomProfile : Profile
    {
        public UserRoomProfile() {
            CreateMap<BookingRequestModel, BookingDto>();
        }
    }
}
