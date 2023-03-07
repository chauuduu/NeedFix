using Application.Service.ClothService;
using Application.Service.Identity;
using Application.Service.OriginsService;
using Application.Service.StaffServices;
using Application.Service.TypeClothService;
using ClothesRentalShop.Profiles;
using Domain.Staffs;
using Infrastructure.IRepository;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var Services = builder.Services;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    // Đọc chuỗi kết nối
    string connectstring = builder.Configuration.GetConnectionString("AppDbContext");
    // Sử dụng MS SQL Server
    options.UseSqlServer(connectstring);
});

builder.Services.AddIdentity<Staff, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddControllersWithViews();

Services.AddTransient<IClothesRepository, ClothesRepository>();
Services.AddTransient<IClothesService,ClothesService>();
Services.AddTransient<ITypeClothesRepository, TypeClothesRepository>();
Services.AddTransient<ITypeClothesService, TypeClothesService>();
Services.AddTransient<IOriginRepository, OriginRepository>();
Services.AddTransient<IOriginService, OriginService>();
Services.AddTransient<IMapperObjectToObject,MapperObjectToObject>();
Services.AddTransient<IStaffRepository, StaffRepository>();
//Services.AddTransient<IStaffService, StaffService>();

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
