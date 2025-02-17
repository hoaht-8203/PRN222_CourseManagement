using CourseManagement.Business.Services.IService;
using CourseManagement.DataAccess.Data;
using CourseManagement.DataAccess.Repositorys.IRepositorys;
using CourseManagement.DataAccess.Repositorys;
using CourseManagement.Model.Model;
using CourseManagementAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequireDigit = false; // Không yêu cầu số
    options.Password.RequireLowercase = false; // Không yêu cầu chữ thường
    options.Password.RequireUppercase = false; // Không yêu cầu chữ hoa
    options.Password.RequireNonAlphanumeric = false; // Không yêu cầu ký tự đặc biệt
    options.Password.RequiredLength = 6; // Độ dài tối thiểu (có thể thay đổi)
    options.Password.RequiredUniqueChars = 0; // Không yêu cầu ký tự duysnhất
});

var configuration = builder.Configuration;
builder.Services.AddDbContext<CourseManagementDb>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBContext"));
});

builder.Services.AddIdentityApiEndpoints<AppUser>().
    AddRoles<IdentityRole>().
    AddEntityFrameworkStores<CourseManagementDb>();

builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
builder.Services.AddScoped<ModuleRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<LessonRepository>();

builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    option.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddCors(option => option.AddPolicy("wasm",
    policy => policy.WithOrigins(builder.Configuration["BackendUrl"] ?? "",
    builder.Configuration["FrontendUrl"] ?? "")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    ));


var app = builder.Build();

app.MapIdentityApi<AppUser>();

app.UseCors("wasm");
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
