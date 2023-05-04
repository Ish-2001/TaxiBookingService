using TaxiBookingServiceUI.Services;
using TaxiBookingServiceUI.UnitOfWork;
//using TaxiBookingServiceUI.Services.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<CityService>();
builder.Services.AddScoped<StateService>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<StateService>();
builder.Services.AddScoped<AreaService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<StatusService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<VehicleCategoryService>();
builder.Services.AddScoped<VehicleDetailService>();
builder.Services.AddScoped<PaymentModeService>();
builder.Services.AddScoped<RideCancellationService>();
builder.Services.AddScoped<UnitOfWorkCity>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<UnitOfWorkBooking>();
builder.Services.AddScoped<UnitOfWorkDriver>();
builder.Services.AddScoped<DriverService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();
