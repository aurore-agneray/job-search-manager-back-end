using JobSearchManagerBack.Configuration;
using JobSearchManagerBack.Data;
using JobSearchManagerBack.Texts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add the connection to the SQL Server database
builder.Services.AddDbContext<SqlServerDbContext>(optionsBuilder =>
{
    string connectionString = builder.Configuration.GetValue<string>("ConnectionString")!;

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new ArgumentException(
            string.Format(InternalErrorTexts.ERROR_MISSING_CONNEXION_STRING, "SQL Server")
        );
    }

    optionsBuilder.UseSqlServer(connectionString);
});

// Add AutoMapper for mapping between DTOs and entities
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>();
});

// Add CORS policy for the front-end domains
string frontEndDomains = builder.Configuration.GetValue<string>("FrontEndDomains")!;
builder.Services.AddCors(BuilderOptions.GetCorsOptions(frontEndDomains));

// Add services to the container.
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

// Configures my API routes and methods
ApiMethods.Configure(app);

app.UseCors();

app.Run();
