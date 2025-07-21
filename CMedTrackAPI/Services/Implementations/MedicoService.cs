using AutoMapper;
using MedTrackAPI.DTOs;
using MedTrackAPI.Models;
using MedTrackAPI.Repositories.Interfaces;
using MedTrackAPI.Services.Interfaces;

namespace MedTrackAPI.Services.Implementations
{
    public class MedicoService : IMedicoService
    {
        private readonly IMedicoRepository _repository;
        private readonly IMapper _mapper;

        public MedicoService(IMedicoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<MedicoDTO>> ListarTodosAsync()
        {
            var medicos = await _repository.ListarTodosAsync();
            return _mapper.Map<List<MedicoDTO>>(medicos);
        }

        public async Task<MedicoDTO?> BuscarPorIdAsync(int id)
        {
            var medico = await _repository.BuscarPorIdAsync(id);
            return medico == null ? null : _mapper.Map<MedicoDTO>(medico);
        }

        public async Task<MedicoDTO> AdicionarAsync(CreateMedicoDTO dto)
        {
            var medico = _mapper.Map<Medico>(dto);
            await _repository.AdicionarAsync(medico);
            return _mapper.Map<MedicoDTO>(medico);
        }

        public async Task<bool> AtualizarAsync(int id, CreateMedicoDTO dto)
        {
            var existe = await _repository.ExisteAsync(id);
            if (!existe) return false;

            var medico = _mapper.Map<Medico>(dto);
            medico.Id = id;

            await _repository.AtualizarAsync(medico);
            return true;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var medico = await _repository.BuscarPorIdAsync(id);
            if (medico == null) return false;

            await _repository.DeletarAsync(medico);
            return true;
        }
    }
}
