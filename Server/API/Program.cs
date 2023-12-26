using System.Text;
using API;
using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

/* builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); */

builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseNpgsql(
        builder.Configuration.GetConnectionString("BetaConnection")
    );
});

builder.Services.AddIdentity<AppUser, AppRole>()
        .AddEntityFrameworkStores<DataContext>()
        .AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])
                    ),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                // If the request is for our hub...
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                    path.StartsWithSegments("/hubs"))
                {
                    // Read the token out of the query string
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddSignalR();


// Repositories
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IRestaurantImageRepository, RestaurantImageRepository>();
builder.Services.AddScoped<IEmployeeImageRepository, EmployeeImageRepository>();
builder.Services.AddScoped<IMenuItemImageRepository, MenuItemImageRepository>();
builder.Services.AddScoped<IOwnerImageRepository, OwnerImageRepository>(); 
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IAppUserNotificationRepository, AppUserNotificationRepository>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IAppUserImageRepository, AppUserImageRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IHubConnectionRepository, HubConnectionRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();





// Services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IOwnerService, OwnerService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IRestaurantImageService, RestaurantImageService>();
builder.Services.AddScoped<IEmployeeImageService, EmployeeImageService>();
builder.Services.AddScoped<IMenuItemImageService, MenuItemImageService>();
builder.Services.AddScoped<IOwnerImageService, OwnerImageService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAppUserImageService, AppUserImageService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IChatService, ChatService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
/* if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} */

app.UseAuthorization();

app.MapControllers();


using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedCountries(context);
    await Seed.SeedCurrency(context);
}
catch (Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An Error occurred during migration");
}

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:4200", "http://localhost:4221"));

app.UseStaticFiles();  // Ovo samo dok je development (kasnije je nginx)

app.MapHub<NotificationHub>("/hubs/notificationHub");
app.MapHub<OrderHub>("/hubs/order-hub");
app.MapHub<ChatHub>("/hubs/chat-hub");

app.Run();
