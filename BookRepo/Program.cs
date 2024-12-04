
using BookRepo.Repo;
using Microsoft.EntityFrameworkCore;

namespace BookRepo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


           builder.Services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins, builder =>
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials());
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var connectionString =
                builder.Configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Connection string"
                    + "'DefaultConnection' not found.");
            builder.Services.AddDbContext<BookDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IBook, BookRepo.Repo.BookRepo>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(MyAllowSpecificOrigins);
            app.UseHttpsRedirection();
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = (context) => ErrorHandlingService.HandleError(context)
            });
            app.UseRouting();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
