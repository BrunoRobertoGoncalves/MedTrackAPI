namespace MedTrackAPI.DTOs
{
    public class CreatePacienteDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Endereco { get; set; } = string.Empty;
    }
}