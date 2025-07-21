
using MedTrackAPI.DTOs;
using MedTrackAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;

namespace MedTrackAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MedicosController : ControllerBase
    {
        private readonly IMedicoService _service;

        public MedicosController(IMedicoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<MedicoDTO>>> GetTodos()
        {
            var Medicos = await _service.ListarTodosAsync();
            return Ok(Medicos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicoDTO>> GetPorID(int id)
        {
            var Medico = await _service.BuscarPorIdAsync(id);
            if(Medico == null)
            {
                return NotFound();
            }
            return Ok(Medico);
        }

        [HttpPost]
        public async Task<ActionResult<MedicoDTO>> Cadastrar(CreateMedicoDTO dto)
        {
            var Medico = await _service.AdicionarAsync(dto);
            return Ok(Medico);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MedicoDTO>> Atualizar(int id, CreateMedicoDTO dto)
        {
            var Atualizado = await _service.AtualizarAsync(id, dto);
            if (!Atualizado)
            {
                return NotFound("Médico não encontrado.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MedicoDTO>> Deletar(int id)
        {
            var Deletado = await _service.DeletarAsync(id);
            if (!Deletado)
            {
                return NotFound("Médico não encontrado.");
            }

            return NoContent();
        }
    }
}