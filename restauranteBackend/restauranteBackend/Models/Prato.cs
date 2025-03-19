namespace RestauranteBackend.Models
{
    public class Prato
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;  // Inicializa com uma string vazia
        public string Ingredientes { get; set; } = string.Empty;  // Inicializa com uma string vazia
    }
}