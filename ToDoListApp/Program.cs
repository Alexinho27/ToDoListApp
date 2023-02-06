using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoListApp.Interfaces;
using ToDoListApp.Services;
using ToDoListApp.ViewModels;

namespace ToDoListApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ToDoDbContext>(options => options.UseInMemoryDatabase("ToDoListDatabase"));
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<ICreateService<ToDoItemViewModel>, ToDoItemService>();
            builder.Services.AddScoped<IEditService<EditToDoItemViewModel>, ToDoItemEditService>();



            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}