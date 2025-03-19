var builder = WebApplication.CreateBuilder(args);

// Adiciona servi�os ao container.
builder.Services.AddControllers();

// Configura o CORS para permitir qualquer origem, m�todo e cabe�alho.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // Permite qualquer origem (Frontend)
              .AllowAnyMethod()  // Permite qualquer m�todo (GET, POST, PUT, DELETE, etc.)
              .AllowAnyHeader(); // Permite qualquer cabe�alho
    });
});

var app = builder.Build();

// Configura o pipeline de requisi��es HTTP.
app.UseHttpsRedirection();

// Habilita o CORS com a pol�tica configurada.
app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();