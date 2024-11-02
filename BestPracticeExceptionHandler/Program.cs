WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddServices();
builder.AddDbContext();

WebApplication app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler("/error");

app.MapControllers();
app.Run();