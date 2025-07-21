namespace MedTrackAPI.DTOs
{
    public class MedicoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CRM { get; set; } = string.Empty;
        public string Especialidade { get; set; } = string.Empty;
    }
}
