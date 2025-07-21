using MedTrackAPI.DTOs;
using MedTrackAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Pacientes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService _service;

        public PacientesController(IPacienteService pacienteService)
        {
            _service = pacienteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PacienteDTO>>> GetTodos()
        {
            var pacientes = await _service.ListarTodosAsync();
            return Ok(pacientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDTO>> GetPorId(int id)
        {
            var paciente = await _service.BuscarPorIdAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            return Ok(paciente);
        }

        [HttpPost]
        public async Task<ActionResult<PacienteDTO>> Cadastrar(CreatePacienteDTO dto)
        {
            var paciente = await _service.AdicionarAsync(dto);
            return Ok(paciente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, CreatePacienteDTO dto)
        {
            var atualizado = await _service.AtualizarAsync(id, dto);
            if (!atualizado)
            {
                return NotFound("Paciente não encontrado.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var deletado = await _service.DeletarAsync(id);
            if (!deletado)
            {
                return NotFound("Paciente não encontrado.");
            }

            return NoContent();
        }
    }
}
