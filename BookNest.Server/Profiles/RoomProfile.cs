using AutoMapper;
using BookNest.Application.Dtos.Rooms;

namespace BookNest.Server.Profiles
{
    public class RoomProfile: Profile
    {
        public RoomProfile() {
            CreateMap<CreateRoomRequestModel, CreateRoomDto>();
            CreateMap<RoomRequestModel, RoomDto>();
        }
    }
}
