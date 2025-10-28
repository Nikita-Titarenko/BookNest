using BookNest.Application.Services;
using BookNest.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookNest.Infrastructure
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IUserRoomService, UserRoomService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            return services;
        }
    }
}
