using DoAnWebBanDoChoi.Data;
using DoAnWebBanDoChoi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseCors("AllowAll");
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    if (!db.Users.Any())
    {
        db.Users.AddRange(
            new User { Name = "Lê Trần Minh Thông", Email = "DH52201514@student.stu.edu.vn", Phone = "0900000001" },
            new User { Name = "Nguyễn Thanh Phú", Email = "DH52201223@student.stu.edu.vn", Phone = "0900000002" },
            new User { Name = "Ngô Hoàng Nam", Email = "DH52201077@student.stu.edu.vn", Phone = "0900000003" },
            new User { Name = "Hoàng Anh Quân", Email = "DH52201284@student.stu.edu.vn", Phone = "0900000003" },
            new User { Name = "Ngô Thái Khang", Email = "DH52201462@student.stu.edu.vn", Phone = "0900000003" },
            new User { Name = "Nguyễn Trường Thành", Email = "DH52200838@student.stu.edu.vn", Phone = "0900000003" }
        );

        db.SaveChanges();
    }
}
app.Run();