using Microsoft.EntityFrameworkCore;
using art_gallery_api.Persistence;
var builder = WebApplication.CreateBuilder(args);

#region Allow-Orgin
    builder.Services.AddCors(c =>
    {
        c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
    });
#endregion

// Add services to the container.
builder.Services.AddControllers();

// -- EF Persistence Layer --
builder.Services.AddDbContext<GalleryContext>();
builder.Services.AddScoped<IArtifactDataAccess, ArtifactEF>();
builder.Services.AddScoped<IUserDataAccess, UserEF>();
builder.Services.AddScoped<IExhibitionDataAccess, ExhibitionEF>();

var app = builder.Build();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    // .AllowCredentials()
);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
