using DfConfig.IService;
using DfConfig.Server.GrpcServices;
using DfConfig.Service;
using DfConfig.Service.Context;
using DfHelper;
using DfHelper.EF;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);
IConfiguration config = builder.Configuration;


// Add services to the container.
builder.Services.AddScoped<DbContextBase>((sp) => {
    return DbContextBase.GetDbContext(EnumHelper.ConvertToEnum<DbType>(config["DbType"]!), config.GetConnectionString("DfConfig")!, config["DbVersion"]!);
});
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdminService, AdminService>();

//添加Grpc服务
builder.Services.AddCodeFirstGrpc();//添加Grpc服务
builder.Services.AddCodeFirstGrpcReflection();//添加Grpc反射

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

//添加Grpc服务路由
app.MapGrpcService<ConfigGrpcService>();
//添加Grpc反射服务 使Grpc服务可被发现服务列表
app.MapCodeFirstGrpcReflectionService();

app.Run();
