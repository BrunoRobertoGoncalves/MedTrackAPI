using MedTrackAPI.Models;

namespace MedTrackAPI.Repositories.Interfaces
{
    public interface IPacienteRepository
    {
        Task<List<Paciente>> ListarTodosAsync();
        Task<Paciente?> BuscarPorIdAsync(int id);
        Task AdicionarAsync(Paciente paciente);
        Task AtualizarAsync(Paciente paciente);
        Task DeletarAsync(Paciente paciente);
        Task<bool> ExisteAsync(int id);
    }
}
