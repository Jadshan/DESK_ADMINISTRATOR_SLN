using Microsoft.EntityFrameworkCore;
using Server_Library.Data;
using Server_Library.Helpers;
using Server_Library.Repos.RepoInterfaces;
using Server_Library.Repos.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
	options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
//strating
builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlConnection") ??
	throw new InvalidOperationException("your connection not found!"));
});

builder.Services.Configure<JwtSection>(builder.Configuration.GetSection("JwtSection"));

//////
builder.Services.AddScoped<IUserAccountRepo, AccountUserRepo>();
///

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

app.Run();
