using MedTrackAPI.Data;
using MedTrackAPI.Models;
using MedTrackAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedTrackAPI.Repositories.Implementations
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly AppDbContext _context;

        public PacienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Paciente>> ListarTodosAsync()
        {
            return await _context.Pacientes.ToListAsync();
        }

        public async Task<Paciente?> BuscarPorIdAsync(int id)
        {
            return await _context.Pacientes.FindAsync(id);
        }

        public async Task AdicionarAsync(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Paciente paciente)
        {
            _context.Pacientes.Update(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(Paciente paciente)
        {
            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Pacientes.AnyAsync(p => p.Id == id);
        }
    }
}
