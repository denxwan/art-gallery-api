using Microsoft.EntityFrameworkCore;
using art_gallery_api.Persistence;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// -- ADO Persistence Layer --
// builder.Services.AddScoped<IRobotCommandDataAccess, RobotCommandADO>();
// builder.Services.AddScoped<IMapDataAccess, MapADO>();

// -- FastMember Persistence Layer --
// builder.Services.AddScoped<IRobotCommandDataAccess, RobotCommandRepository>();
// builder.Services.AddScoped<IMapDataAccess, MapRepository>();

// -- EF Persistence Layer --
builder.Services.AddDbContext<GalleryContext>();
builder.Services.AddScoped<IArtifactDataAccess, ArtifactEF>();
builder.Services.AddScoped<IUserDataAccess, UserEF>();
builder.Services.AddScoped<IExhibitionDataAccess, ExhibitionEF>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
