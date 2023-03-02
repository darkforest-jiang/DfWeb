using DfConfig.IService;
using DfConfig.Service;
using DfConfig.Service.Context;
using DfHelper;
using DfHelper.EF;

var builder = WebApplication.CreateBuilder(args);
IConfiguration config = builder.Configuration;


// Add services to the container.
builder.Services.AddScoped<DbContextBase>((sp) => {
    return DbContextBase.GetDbContext(EnumHelper.ConvertToEnum<DbType>(config["DbType"]!), config.GetConnectionString("DfConfig")!, config["DbVersion"]!);
});
builder.Services.AddScoped<IUserService, UserService>();

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

app.Run();
