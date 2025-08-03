using BibliotecaApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ... (toda a sua configuração de CORS, DbContext e Controllers continua a mesma)
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173") 
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseInMemoryDatabase("BibliotecaDb"));

builder.Services.AddControllers();


var app = builder.Build();

// ... (o bloco de SeedData continua o mesmo)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApiDbContext>();
        SeedData.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Um erro ocorreu ao popular o banco de dados.");
    }
}



app.UseCors(MyAllowSpecificOrigins);


app.UseDefaultFiles(); 
app.UseStaticFiles(); 

app.UseAuthorization();
app.MapControllers();


app.MapFallbackToFile("index.html");

app.Run();