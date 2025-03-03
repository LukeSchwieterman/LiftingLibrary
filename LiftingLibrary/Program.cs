using LiftingLibrary.Models;
using LiftingLibrary.Security;


namespace LiftingLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSession();
            builder.Services.AddControllersWithViews();
            string connectionString = builder.Configuration.GetConnectionString("Project");

            builder.Services.AddSingleton<ITokenGenerator>(tk => new JwtGenerator(builder.Configuration["JwtSecret"]));
            builder.Services.AddSingleton<IPasswordHasher>(ph => new PasswordHasher());
            builder.Services.AddTransient<DAO.User.IUserDAO>(e => new DAO.User.UserDAO(connectionString));
            builder.Services.AddTransient<DAO.Workouts.IWorkoutsDAO>(e => new DAO.Workouts.WorkoutsDAO(connectionString));
            builder.Services.AddTransient<DAO.WorkoutDetails.IWorkoutDetailsDAO>(e => new DAO.WorkoutDetails.WorkoutDetailsDAO(connectionString));
            builder.Services.AddSingleton(gu => new GlobalUser());

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
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
