using ShoppingCart.Models;


namespace ShoppingCart
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();
            builder.Services.AddSingleton<DbContext>();
            var app = builder.Build();

           
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                
                app.UseHsts();
            }


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Software}/{action=Index}/{id?}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            
            
            app.Run();
        }
    }
}