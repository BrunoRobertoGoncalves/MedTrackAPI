using MedTrackAPI.DTOs;

namespace MedTrackAPI.Services.Interfaces
{
    public interface IPacienteService
    {
        Task<List<PacienteDTO>> ListarTodosAsync();
        Task<PacienteDTO?> BuscarPorIdAsync(int id);
        Task<PacienteDTO> AdicionarAsync(CreatePacienteDTO dto);
        Task<bool> AtualizarAsync(int id, CreatePacienteDTO pacienteAtualizado);
        Task<bool> DeletarAsync(int id);
    }
}
