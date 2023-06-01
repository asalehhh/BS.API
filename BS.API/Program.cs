using BS.API.Infrastructure;
using BS.DataLayer;
using BS.DataLayer.BusinessLayer.Repositories.UserAuthentication;
using BS.DataLayer.Data.Repositories.Account;
using BS.DataLayer.Data.Repositories.Transaction;
using BS.DataLayer.Data.Repositories.UserProfile;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "BSPolicy",
                      b =>
                      {
                          b
                            .WithOrigins(builder.Configuration.GetSection("AllowedOrigin").Value)
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                      });
});

builder.Services.AddControllers();
builder.Services.AddSingleton<DapperContext>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add db context and identity models
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")), ServiceLifetime.Singleton);
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

//Inject dependecies
builder.Services.AddSingleton<IUserAuthenticationManager, UserAuthenticationManager>();
builder.Services.AddSingleton<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddSingleton<IAccountRepository, AccountRepository>();
builder.Services.AddSingleton<ITransactionRepository, TransactionRepository>();

//Implement infrastructure config
AuthorizationConfig.configureJWT(builder);
SwaggerConfig.ConfigureSwagger(builder);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors("BSPolicy");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();




app.Run();
