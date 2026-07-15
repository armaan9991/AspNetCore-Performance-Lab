// created application builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();`
    app.UseSwaggerUI();
}
// redirectes HTTP requests to HTTPS
app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


// Application gets started here and listens for incoming requests
app.Run();



// Program.cs is basically entrypoint of the application. It is responsible for configuring and starting the web application. The code sets up services, middleware, and endpoints for handling HTTP requests.
// In this case, it configures Swagger for API documentation, sets up HTTPS redirection, and
// defines a simple weather forecast endpoint. Finally, it starts the application and listens for incoming requests.
