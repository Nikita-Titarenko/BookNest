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

    public virtual DbSet<AppUser> AppUsers { get; set; }

    public virtual DbSet<AppUserRoom> AppUserRooms { get; set; }

    public virtual DbSet<AuditAppUserRoom> AuditAppUserRooms { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<HotelListItemDto> HotelListItems { get; set; }

    public virtual DbSet<RoomListItemDto> RoomListItems { get; set; }

    public virtual DbSet<HotelWithRoomListItemDto> HotelWithRoomListItems { get; set; }

    public virtual DbSet<RoomBookingDto> RoomBookings { get; set; }

    public virtual DbSet<RoomBookingByHotelDto> RoomBookingByHotel { get; set; }

    public virtual DbSet<AuditRoomBookingsByHotelDto> AuditRoomBookingsByHotel { get; set; }

    public int? Login(string email, string password)
    {
        throw new NotImplementedException();
    }

    public string? GetHotelName(int hotelId)
    {
        throw new NotImplementedException();
    }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-I2MBG2Q;Database=BookNestDb;User Id=BookNestUser;Password=77228Glnik;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
            entity.HasKey(e => e.AppUserId).HasName("PK__AppUser__06D526143E40DD46");

            entity.ToTable("AppUser");

            entity.Property(e => e.AppUserId).HasColumnName("app_user_id");
            entity.Property(e => e.AppUserEmail)
                .HasMaxLength(254)
                .HasColumnName("app_user_email");
            entity.Property(e => e.AppUserFullname)
                .HasMaxLength(150)
                .HasColumnName("app_user_fullname");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(32)
                .HasColumnName("password_hash");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(32)
                .HasColumnName("password_salt");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<AppUserRoom>(entity =>
        {
            entity.HasKey(e => new { e.AppUserId, e.RoomId }).HasName("PK__AppUserR__B74353BC6E32B03D");

            entity.ToTable("AppUserRoom", tb =>
                {
                    tb.HasTrigger("trg_AppUserRoom_AfterDelete");
                    tb.HasTrigger("trg_AppUserRoom_AfterInsert");
                    tb.HasTrigger("trg_AppUserRoom_AfterUpdate");
                });

            entity.Property(e => e.AppUserId).HasColumnName("app_user_id");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.AppUser).WithMany(p => p.AppUserRooms)
                .HasForeignKey(d => d.AppUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AppUserRo__app_u__440B1D61");

            entity.HasOne(d => d.Room).WithMany(p => p.AppUserRooms)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AppUserRo__room___44FF419A");
        });

        modelBuilder.Entity<AuditAppUserRoom>(entity =>
        {
            entity.HasKey(e => e.AuditAppUserRoomId).HasName("PK__AuditApp__677042CE31E299CF");

            entity.ToTable("AuditAppUserRoom");

            entity.Property(e => e.AuditAppUserRoomId).HasColumnName("audit_app_user_room_id");
            entity.Property(e => e.ActionDateTime).HasColumnName("action_date_time");
            entity.Property(e => e.ActionType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("action_type");
            entity.Property(e => e.AppUserId).HasColumnName("app_user_id");
            entity.Property(e => e.NewEndDate).HasColumnName("new_end_date");
            entity.Property(e => e.NewStartDate).HasColumnName("new_start_date");
            entity.Property(e => e.OldEndDate).HasColumnName("old_end_date");
            entity.Property(e => e.OldStartDate).HasColumnName("old_start_date");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.HotelId).HasName("PK__Hotel__45FE7E267C8697DF");

            entity.ToTable("Hotel");

            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.AppUserId).HasColumnName("app_user_id");
            entity.Property(e => e.HotelCity)
                .HasMaxLength(200)
                .HasColumnName("hotel_city");
            entity.Property(e => e.HotelDescription)
                .HasMaxLength(3000)
                .HasColumnName("hotel_description");
            entity.Property(e => e.HotelName)
                .HasMaxLength(100)
                .HasColumnName("hotel_name");

            entity.HasOne(d => d.AppUser).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.AppUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Hotel__app_user___5BE2A6F2");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Room__19675A8A35B4A296");

            entity.ToTable("Room");

            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.GuestsNumber).HasColumnName("guests_number");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.RoomDescription)
                .HasMaxLength(3000)
                .HasColumnName("room_description");
            entity.Property(e => e.RoomName)
                .HasMaxLength(200)
                .HasColumnName("room_name");
            entity.Property(e => e.RoomPrice).HasColumnName("room_price");
            entity.Property(e => e.RoomQuantity).HasColumnName("room_quantity");
            entity.Property(e => e.RoomSize)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("room_size");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK__Room__hotel_id__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
