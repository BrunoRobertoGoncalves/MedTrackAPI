using MedTrackAPI.DTOs;

namespace MedTrackAPI.Services.Interfaces
{
    public interface IMedicoService
    {
        Task<List<MedicoDTO>> ListarTodosAsync();
        Task<MedicoDTO?> BuscarPorIdAsync(int id);
        Task<MedicoDTO> AdicionarAsync(CreateMedicoDTO dto);
        Task<bool> AtualizarAsync(int id, CreateMedicoDTO dto);
        Task<bool> DeletarAsync(int id);
    }
}
