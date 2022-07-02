using Blog.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(op => op.SuppressModelStateInvalidFilter = true);
builder.Services.AddDbContext<BlogDataContext>();

var app = builder.Build();

app.MapControllers();

app.Run();
