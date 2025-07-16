
using MedTrackAPI.Data;
using MedTrackAPI.Models;
using MedTrackAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedTrackAPI.Repositories.Implementations
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly AppDbContext _context;

        public MedicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Medico>> ListarTodosAsync()
        {
            return await _context.Medicos.ToListAsync();
        }

        public async Task<Medico?> BuscarPorIdAsync(int id)
        {
            return await _context.Medicos.FindAsync(id);
        }

        public async Task AdicionarAsync(Medico medico)
        {
            _context.Medicos.Add(medico);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Medico medico)
        {
            _context.Medicos.Update(medico);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeletarAsync(Medico medico)
        {
            _context.Medicos.Remove(medico);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Medicos.AnyAsync(m => m.Id == id);
        }
    }
}