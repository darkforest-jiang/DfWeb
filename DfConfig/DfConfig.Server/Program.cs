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

//���Grpc����
builder.Services.AddCodeFirstGrpc();//���Grpc����
builder.Services.AddCodeFirstGrpcReflection();//���Grpc����

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

//���Grpc����·��
app.MapGrpcService<ConfigGrpcService>();
//���Grpc������� ʹGrpc����ɱ����ַ����б�
app.MapCodeFirstGrpcReflectionService();

app.Run();
