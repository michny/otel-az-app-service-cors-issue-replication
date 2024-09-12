using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
                      {
                          policy.WithOrigins("https://lemon-coast-00e795f03.5.azurestaticapps.net");
                      });
});

// Setup OpenTelemetry Tracing
builder.Services.AddOpenTelemetry().WithTracing(builder =>
{
    //builder.SetSampler(new AlwaysOnSampler());

    builder
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("tracing-poc"))
        // Configure ASP.NET Core Instrumentation
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddConsoleExporter();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
