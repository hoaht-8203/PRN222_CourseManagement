﻿using CourseManagement.Business.Services;
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
using CourseManagementAPI.Mappings;
using CourseManagementAPI.Hubs;
using Amazon.S3;
using CourseManagement.Model.Mail;

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
builder.Services.AddSignalR();

builder.Services.AddIdentityApiEndpoints<AppUser>().
    AddRoles<IdentityRole>().
    AddEntityFrameworkStores<CourseManagementDb>();

builder.Services.AddScoped<MinioFileService>();
builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
builder.Services.AddScoped<ModuleRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<LessonRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<CommentRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<DocumentRepository>();

builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddHttpClient();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton<MailService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
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
    .WithExposedHeaders("Content-Disposition")
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

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<CommentHub>("/commentHub"); // SignalR Hub
});

app.Run();
