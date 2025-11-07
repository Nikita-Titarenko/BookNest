using AutoMapper;
using BookNest.Application.Dtos.Hotels;
using BookNest.Server.Requests.Hotels;

namespace BookNest.Server.Profiles
{
    public class HotelProfile : Profile
    {
        public HotelProfile() {
            CreateMap<HotelRequestModel, HotelDto>();
        }
    }
}
