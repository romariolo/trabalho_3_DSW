namespace BibliotecaApi.Dtos
{
    public class SessaoCreateDto
    {
        public string IdLivro { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}