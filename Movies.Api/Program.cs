using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movies.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MoviesApiContext>(options =>
    options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("MoviesApiContext") ?? 
        throw new InvalidOperationException("Connection string 'MoviesApiContext' not found.")));

builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:7010";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

builder.Services.AddAuthorization(options =>
                {
                    options.AddPolicy("ClientIdPolicy", policy => 
                        policy.RequireClaim("client_id", "movieClient", "movies_mvc_client"));
                });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

SeedDatabase(app);

void SeedDatabase(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var moviesContext = services.GetRequiredService<MoviesApiContext>();
            MoviesContextSeed.SeedAsync(moviesContext);
        }
}

app.Run();
