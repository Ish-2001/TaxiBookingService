//using TaxiBookingService;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NLog;
using System.Text;
using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.DAL.UnitOfWork.UnitOfWork;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Logging;
using TaxiBookingService.Services.Interfaces;
using TaxiBookingService.Services.Service;

public class Program
{
    static void Main(string[] args)
    {
        //CreateHostBuilder().Build().Run();
        LogManager.LoadConfiguration(System.String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<TaxiContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr"))
       );

        //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<TaxiBookingContext>();
        builder.Services.AddControllers().AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        });


        builder.Services.AddSingleton<ILog, NLogging>();
        builder.Services.AddScoped<TaxiContext>();

        // services.AddControllersWithViews();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IJwtService, JwtService>();
        builder.Services.AddScoped<IHashingService, HashingService>();
        builder.Services.AddScoped<IUnitOfWorkArea, UnitOfWorkArea>();
        builder.Services.AddScoped<IUnitOfWorkBooking, UnitOfWorkBooking>();
        builder.Services.AddScoped<IUnitOfWorkBookingStatus, UnitOfWorkBookingStatus>();
        builder.Services.AddScoped<IUnitOfWorkCancellationReason, UnitOfWorkCancellationReason>();
        builder.Services.AddScoped<IUnitOfWorkCity, UnitOfWorkCity>();
        builder.Services.AddScoped<IUnitOfWorkDriver, UnitOfWorkDriver>();
        builder.Services.AddScoped<IUnitOfWorkLocation, UnitOfWorkLocation>();
        builder.Services.AddScoped<IUnitOfWorkPayment, UnitOfWorkPayment>();
        builder.Services.AddScoped<IUnitOfWorkPaymentMode, UnitOfWorkPaymentMode>();
        builder.Services.AddScoped<IUnitOfWorkState, UnitOfWorkState>();
        builder.Services.AddScoped<IUnitOfWorkUserRole, UnitOfWorkUserRole>();
        builder.Services.AddScoped<IUnitOfWorkUser, UnitOfWorkUser>();
        builder.Services.AddScoped<IUnitOfWorkVehicleCategory, UnitOfWorkVehicleCategory>();
        builder.Services.AddScoped<IUnitOfWorkVehicleDetails, UnitOfWorkVehicleDetails>();
        builder.Services.AddScoped<IUnitOfWorkRideCancellation , UnitOfWorkRideCancellation>();
        builder.Services.AddScoped<IUnitOfWorkDriverRating, UnitOfWorkDriverRating>();
        builder.Services.AddScoped<IBookingStatusService, BookingStatusService>();
        builder.Services.AddScoped<ICancellationReasonService, CancellationReasonService>();
        builder.Services.AddScoped<ICityService, CityService>();
        builder.Services.AddScoped<IStateService, StateService>();
        builder.Services.AddScoped<IAreaService, AreaService>();
        builder.Services.AddScoped<IBookingService, BookingService>();
        builder.Services.AddScoped<IDriverService, DriverService>();
        builder.Services.AddScoped<IVehicleCategoryService, VehicleCategoryService>();
        builder.Services.AddScoped<IVehicleDetailsService, VehicleDetailsService>();
        builder.Services.AddScoped<ILocationService, LocationService>();
        builder.Services.AddScoped<IPaymentModeService, PaymentModeService>();
        builder.Services.AddScoped<IRoleService, RoleService>();
        builder.Services.AddScoped<IAdminService, AdminService>();
        builder.Services.AddScoped<IPaymentService, PaymentService>();
        builder.Services.AddScoped<IRideCancellationService, RideCancellationService>();
        builder.Services.AddScoped<IDriverRatingService, DriverRatingService>();
        builder.Services.AddScoped<IDriverComplaintService, DriverComplaintService>();
        builder.Services.AddScoped<IUnitOfWorkDriverComplaint , UnitOfWorkDriverComplaint>();


        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Taxi Booking Service", Version = "v1" });
        });

        builder.Services.AddSwaggerGen(option =>
        {
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
                     {
                          {
                                new OpenApiSecurityScheme
                                  {
                                      Reference = new OpenApiReference
                                      {
                                          Type = ReferenceType.SecurityScheme,
                                          Id = "Bearer"
                                      }
                                  },
                                  new string[] {}
                          }
                     });

        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }
   
}