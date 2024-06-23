public class Candidato
{
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string Telefone { get; set; }
    public string Cidade { get; set; }
    public string Linkedin { get; set; }
    public string Email { get; set; }
    public IFormFile Curriculo { get; set; }
    public string Mensagem { get; set; }
    public string? FilePath { get; set; }
    public string? FileName { get; set; }
}