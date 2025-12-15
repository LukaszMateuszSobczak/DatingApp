using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services
// wszystkie services które dodajemy są automatcznie dostępne do dependency injections
builder.Services.AddControllers();

// *dodajemy naszą baze do services, musimy podać opcje czyli konfiguracje bazy
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    //* Connection string to po prostu łańcuch znaków, który mówi aplikacji, jak połączyć się z bazą danych. Można go traktować jak „adres i klucz do skrzynki pocztowej” – mówi programowi: gdzie jest baza danych, jak się nazywa, jaki login i hasło użyć oraz jakie dodatkowe opcje włączyć.

    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")); //* należy w pliku appsettings stworzyć Connection String
});

builder.Services.AddCors();

builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

//Configure a HTTP

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));
app.MapControllers();


app.Run();
