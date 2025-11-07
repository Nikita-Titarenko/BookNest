using BookNest.Infrastructure;
using BookNest.Server.Attributes;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowLocalHost",
        policy =>
        {
            policy.WithOrigins("https://localhost:61085")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

builder.Services.AddAutoMapper(c => { }, AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value != null && e.Value.Errors.Count > 0)
            .Select(e =>
            {
                var error = e.Value!.Errors.First().ErrorMessage;
                var parts = error.Split(':', 2);
                return new
                {
                    Field = char.ToLower(e.Key[0]) + e.Key.Substring(1),
                    Code = parts.Length > 1 && int.TryParse(parts[0], out var code) ? code : 0,
                    Message = parts.Length > 1 ? parts[1] : error
                };
            });

        return new BadRequestObjectResult(new { errors });
    };
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.UseCors("AllowLocalHost");

app.Run();
