using AutoMapper;
using MedTrackAPI.DTOs;
using MedTrackAPI.Models;
using MedTrackAPI.Repositories.Interfaces;
using MedTrackAPI.Services.Interfaces;

namespace MedTrackAPI.Services.Implementations
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _repository;
        private readonly IMapper _mapper;

        public PacienteService(IPacienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<PacienteDTO>> ListarTodosAsync()
        {
            var pacientes = await _repository.ListarTodosAsync();
            return _mapper.Map<List<PacienteDTO>>(pacientes);
        }

        public async Task<PacienteDTO?> BuscarPorIdAsync(int id)
        {
            var paciente = await _repository.BuscarPorIdAsync(id);
            if (paciente == null) return null;

            return _mapper.Map<PacienteDTO>(paciente); 
        }

        public async Task<PacienteDTO> AdicionarAsync(CreatePacienteDTO dto)
        {
            var paciente = _mapper.Map<Paciente>(dto);

            await _repository.AdicionarAsync(paciente);

            return _mapper.Map<PacienteDTO>(paciente);
        }

        public async Task<bool> AtualizarAsync(int id, CreatePacienteDTO pacienteAtualizado)
        {
            var pacienteExistente = await _repository.BuscarPorIdAsync(id);
            if (pacienteExistente == null) return false;

            pacienteExistente.Nome = pacienteAtualizado.Nome;
            pacienteExistente.CPF = pacienteAtualizado.CPF;
            pacienteExistente.DataNascimento = pacienteAtualizado.DataNascimento;
            pacienteExistente.Endereco = pacienteAtualizado.Endereco;

            await _repository.AtualizarAsync(pacienteExistente);
            return true;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var paciente = await _repository.BuscarPorIdAsync(id);
            if (paciente == null) return false;

            await _repository.DeletarAsync(paciente);
            return true;
        }
    }
}
