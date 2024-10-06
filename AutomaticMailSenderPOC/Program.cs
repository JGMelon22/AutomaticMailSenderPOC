using AutomaticMailSenderPOC.Interfaces;
using AutomaticMailSenderPOC.Models;
using AutomaticMailSenderPOC.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MailSender POC API",
        Description = "An ASP.NET Core Web API for Send E-mails",
        TermsOfService = new Uri(string.Empty),
        Contact = new OpenApiContact
        {
            Name = "GitHub Page",
            Url = new Uri("https://github.com/JGMelon22")
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://example.com/license")
        }
    });
});

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddScoped<ISendEmail, SendEmail>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
