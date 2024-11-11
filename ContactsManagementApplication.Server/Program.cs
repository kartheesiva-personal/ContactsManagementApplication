using ContactsManagementApplication.Server.Middlewares;
using ContactsManagementApplication.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ContactService>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowHttpAndHttps",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200", "https://localhost:4200")  // Allow both HTTP and HTTPS
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowHttpAndHttps");
app.MapControllers();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.Run();
