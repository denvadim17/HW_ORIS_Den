using Microsoft.EntityFrameworkCore;
using PokemonApi;
using PokemonApi.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlPath = string.Format(@"{0}PokemonApi.xml", AppDomain.CurrentDomain.BaseDirectory);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultValue"));
});


builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dataSeeder = services.GetRequiredService<DataSeeder>();
        dataSeeder.Seed(); 
    }
    catch (Exception ex)
    {
        
        Console.WriteLine("An error occurred while seeding the database.");
        Console.WriteLine(ex.Message);
    }
}

app.Run();