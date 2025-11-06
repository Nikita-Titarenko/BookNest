using BookNest.Application.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Infrastructure;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<AppUser> AppUsers { get; set; }

    public DbSet<CreateAppUserRoomResultDto> CreateAppUserRooms { get; set; }

    public DbSet<CreateHotelResultDto> CreateHotelResults { get; set; }

    public DbSet<CreateRoomResultDto> CreateRoomResults { get; set; }

    public DbSet<AppUserDto> AppUserDtos { get; set; }

    public int? Login(string email, string password)
    {
        throw new NotImplementedException();
    }

    public string? GetHotelName(int hotelId)
    {
        throw new NotImplementedException();
    }

    public IQueryable<AppUserDto> GetAppUser(int userId)
        => FromExpression(() => GetAppUser(userId));

    public IQueryable<HotelListItemDto> GetHotelsByUser(int userId)
        => FromExpression(() => GetHotelsByUser(userId));

    public IQueryable<RoomBookingByHotelDto> GetRoomBookingsByHotel(int userId, int hotelId)
        => FromExpression(() => GetRoomBookingsByHotel(userId, hotelId));

    public IQueryable<AuditRoomBookingsByHotelDto> GetAuditRoomBookingsByHotel(int userId, int hotelId)
        => FromExpression(() => GetAuditRoomBookingsByHotel(userId, hotelId));

    public IQueryable<HotelDto> GetHotel(int hotelId)
        => FromExpression(() => GetHotel(hotelId));

    public IQueryable<RoomListItemDto> GetRoom(int roomId)
        => FromExpression(() => GetRoom(roomId));

    public IQueryable<RoomBookingDto> GetRoomBooking(int appUserId, int roomId)
        => FromExpression(() => GetRoomBooking(appUserId, roomId));

    public IQueryable<RoomListItemDto> GetRoomsByHotel(int hotelId, DateTime? startDateTime = null!, DateTime? endDateTime = null!, int? guestsNumber = null!)
        => FromExpression(() => GetRoomsByHotel(hotelId, startDateTime, endDateTime, guestsNumber));

    public IQueryable<HotelWithRoomListItemDto> GetHotelsWithCheapestRoom(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, int? guestsNumber = null!)
        => FromExpression(() => GetHotelsWithCheapestRoom(startDate, endDate, pageNumber, pageSize, guestsNumber));

    public IQueryable<HotelWithRoomListItemDto> GetHotelsWithMostExpensiveRoom(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, int? guestsNumber = null!)
        => FromExpression(() => GetHotelsWithMostExpensiveRoom(startDate, endDate, pageNumber, pageSize, guestsNumber));

    public IQueryable<RoomBookingByUserDto> GetRoomBookingsByUser(int userId)
    => FromExpression(() => GetRoomBookingsByUser(userId));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CreateAppUserRoomResultDto>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.UserId).HasColumnName("app_user_id");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
        });

        modelBuilder.Entity<HotelDto>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.HotelName).HasColumnName("hotel_name");
            entity.Property(e => e.HotelCity).HasColumnName("hotel_city");
            entity.Property(e => e.HotelDescription).HasColumnName("hotel_description");
        });

        modelBuilder.Entity<HotelListItemDto>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.HotelName).HasColumnName("hotel_name");
            entity.Property(e => e.HotelCity).HasColumnName("hotel_city");
            entity.Property(e => e.TotalRoomCount).HasColumnName("total_room_count");
        });

        modelBuilder.Entity<RoomBookingByUserDto>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.HotelName).HasColumnName("hotel_name");
            entity.Property(e => e.RoomName).HasColumnName("room_name");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
        });

        modelBuilder.Entity<RoomListItemDto>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.RoomName).HasColumnName("room_name");
            entity.Property(e => e.RoomPrice).HasColumnName("room_price");
            entity.Property(e => e.RoomSize).HasColumnName("room_size");
            entity.Property(e => e.RoomQuantity).HasColumnName("room_quantity");
            entity.Property(e => e.GuestsNumber).HasColumnName("guests_number");
        });

        modelBuilder.Entity<HotelWithRoomListItemDto>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.HotelName).HasColumnName("hotel_name");
            entity.Property(e => e.HotelCity).HasColumnName("hotel_city");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.RoomName).HasColumnName("room_name");
            entity.Property(e => e.RoomPrice).HasColumnName("room_price");
        });

        modelBuilder.Entity<RoomBookingDto>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
        });

        modelBuilder.Entity<RoomBookingByHotelDto>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.RoomName).HasColumnName("room_name");
            entity.Property(e => e.AppUserEmail).HasColumnName("app_user_email");
            entity.Property(e => e.AppUserFullname).HasColumnName("app_user_fullname");
            entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
            entity.Property(e => e.BookingsTotalCount).HasColumnName("bookings_total_count");
        });

        modelBuilder.Entity<AuditRoomBookingsByHotelDto>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.AuditAppUserRoomId).HasColumnName("audit_app_user_room_id");
            entity.Property(e => e.OldStartDate).HasColumnName("old_start_date");
            entity.Property(e => e.NewStartDate).HasColumnName("new_start_date");
            entity.Property(e => e.OldEndDate).HasColumnName("old_end_date");
            entity.Property(e => e.NewEndDate).HasColumnName("new_end_date");
            entity.Property(e => e.ActionDateTime).HasColumnName("action_date_time");
            entity.Property(e => e.ActionType).HasColumnName("action_type");
            entity.Property(e => e.RoomName).HasColumnName("room_name");
            entity.Property(e => e.AppUserEmail).HasColumnName("app_user_email");
            entity.Property(e => e.AppUserFullname).HasColumnName("app_user_fullname");
            entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
        });

        modelBuilder
            .HasDbFunction(() => Login(default!, default!))
            .HasName("Login")
            .HasSchema("dbo");

        modelBuilder
            .HasDbFunction(() => GetAppUser(default!))
            .HasName("GetAppUser")
            .HasSchema("dbo");

        modelBuilder
            .HasDbFunction(() => GetRoomBooking(default!, default!))
            .HasName("GetRoomBooking")
            .HasSchema("dbo");

        modelBuilder
            .HasDbFunction(() => GetRoomBookingsByHotel(default!, default!))
            .HasName("GetRoomBookingsByHotel")
            .HasSchema("dbo");

        modelBuilder
            .HasDbFunction(() => GetAuditRoomBookingsByHotel(default!, default!))
            .HasName("GetAuditRoomBookingsByHotel")
            .HasSchema("dbo");

        modelBuilder
            .HasDbFunction(() => GetRoomBookingsByUser(default!))
            .HasName("GetRoomBookingsByUser")
            .HasSchema("dbo");

        modelBuilder
            .HasDbFunction(() => GetRoom(default!))
            .HasName("GetRoom")
            .HasSchema("dbo");

        modelBuilder
            .HasDbFunction(() => GetHotel(default!))
            .HasName("GetHotel")
            .HasSchema("dbo");

        modelBuilder
            .HasDbFunction(() => GetHotelName(default!))
            .HasName("GetHotelName")
            .HasSchema("dbo");

        modelBuilder
            .HasDbFunction(() => GetHotelsByUser(default!))
            .HasName("GetHotelsByUser")
            .HasSchema("dbo");

        modelBuilder
            .HasDbFunction(() => GetRoomsByHotel(default!, default!, default!, default!))
            .HasName("GetRoomsByHotel")
            .HasSchema("dbo");

        modelBuilder
            .HasDbFunction(() => GetHotelsWithCheapestRoom(default!, default!, default!, default!, default!))
            .HasName("GetHotelsWithCheapestRoom")
            .HasSchema("dbo");

        modelBuilder
            .HasDbFunction(() => GetHotelsWithMostExpensiveRoom(default!, default!, default!, default!, default!))
            .HasName("GetHotelsWithMostExpensiveRoom")
            .HasSchema("dbo");

        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AppUser__06D526143E40DD46");

            entity.ToTable("AppUser");
        });

        modelBuilder.Entity<CreateRoomResultDto>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.RoomName).HasColumnName("room_name");
            entity.Property(e => e.RoomPrice).HasColumnName("room_price");
            entity.Property(e => e.RoomSize).HasColumnName("room_size");
            entity.Property(e => e.RoomQuantity).HasColumnName("room_quantity");
            entity.Property(e => e.GuestsNumber).HasColumnName("guests_number");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
        });

        modelBuilder.Entity<CreateHotelResultDto>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.HotelName).HasColumnName("hotel_name");
            entity.Property(e => e.HotelCity).HasColumnName("hotel_city");
            entity.Property(e => e.HotelDescription).HasColumnName("hotel_description");
            entity.Property(e => e.AppUserId).HasColumnName("app_user_id");
        });

        modelBuilder.Entity<AppUserDto>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.AppUserId).HasColumnName("app_user_id");
            entity.Property(e => e.AppUserEmail).HasColumnName("app_user_email");
            entity.Property(e => e.AppUserFullname).HasColumnName("app_user_fullname");
            entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
