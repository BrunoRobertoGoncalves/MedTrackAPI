using MedTrackAPI.Models;

namespace MedTrackAPI.Repositories.Interfaces
{
    public interface IMedicoRepository
    {
        Task<List<Medico>> ListarTodosAsync();
        Task<Medico?> BuscarPorIdAsync(int id);
        Task AdicionarAsync(Medico medico);
        Task AtualizarAsync(Medico medico);
        Task DeletarAsync(Medico medico);
        Task<bool> ExisteAsync(int id);
    }
}
