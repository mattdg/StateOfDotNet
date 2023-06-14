using ChatSample.Hubs;

var builder = WebApplication.CreateBuilder();

// Register stuff
builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseFileServer();  // To also host our wwwroot files.
app.UseRouting();

app.MapHub<ChatHub>("/chat"); 

// GO!
app.Run();