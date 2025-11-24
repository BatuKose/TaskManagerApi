using Entites.Data_Transfer_object;
using Services.Contracts;
using TaskManagerApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();

var app = builder.Build();
//using (var scope = app.Services.CreateScope())
//{
//    var serviceManager = scope.ServiceProvider.GetRequiredService<IServiceManager>();

//    var dto = new CreateUserDto
//    {
//        userName = "Spartan",
//        Email = "spartan@test.com",
//        Password = "123456",
//        RoleId = 1,
//        id=1
//    };

  
//    await serviceManager.UserService.CreateUserAsync(dto);

//    Console.WriteLine("Debug: User oluþturuldu.");
//}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
