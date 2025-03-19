var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao container.
builder.Services.AddControllers();

// Configura o CORS para permitir qualquer origem, método e cabeçalho.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // Permite qualquer origem (Frontend)
              .AllowAnyMethod()  // Permite qualquer método (GET, POST, PUT, DELETE, etc.)
              .AllowAnyHeader(); // Permite qualquer cabeçalho
    });
});

var app = builder.Build();

// Configura o pipeline de requisições HTTP.
app.UseHttpsRedirection();

// Habilita o CORS com a política configurada.
app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();