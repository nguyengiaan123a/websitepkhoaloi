using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using websitepkhoaloi.Data;
using websitepkhoaloi.Helpper;
using websitepkhoaloi.Models.Enitity;
using websitepkhoaloi.Services.Interface;
using websitepkhoaloi.Services.Responsive;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyDbcontext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 30, // Số lần thử lại tối đa
                maxRetryDelay: TimeSpan.FromSeconds(10), // Thời gian chờ tối đa giữa các lần thử lại
                errorNumbersToAdd: null); // Có thể chỉ định mã lỗi cụ thể để thử lại
        }));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<MyDbcontext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<IUser,UserResponsive>();
builder.Services.AddScoped<ITitlemenu,TitleMenuResponsive>();
builder.Services.AddScoped<IMenu,MenuResponsive>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.UseStaticFiles();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
