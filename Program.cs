using LibFlow.Data;
using LibFlow.Models;
using LibFlow.Services;
using LibFlow.Services.Author;
using LibFlow.Services.Book;
using LibFlow.Services.Email;
using LibFlow.Services.RabbitMq;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registra os serviços
builder.Services.AddScoped<IAuthorInterface, AuthorService>();
builder.Services.AddScoped<IBookInterface, BookService>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Registra RabbitMQ
builder.Services.AddSingleton<RabbitMqService>();
builder.Services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();
builder.Services.AddSingleton<BookReservationConsumer>();
builder.Services.Configure<SmtpSettings>(builder.Configuration
                                         .GetSection("SmtpSettings"));
builder.Services.AddSingleton<ISendEmail, SendEmail>();

var app = builder.Build();

// Configuração do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Obtém a instância do consumidor e inicia a escuta da fila
var consumer = app.Services.GetRequiredService<BookReservationConsumer>();
Task.Run(() => consumer.StartListening());

app.Run();
