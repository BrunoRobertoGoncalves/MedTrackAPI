using AutoMapper;
using MedTrackAPI.DTOs;
using MedTrackAPI.Models;
using MedTrackAPI.Repositories.Interfaces;
using MedTrackAPI.Services.Implementations;
using Moq;
using Xunit;

namespace MedTrackAPI.Tests.ServicesTests
{
    public class PacienteServiceTests
    {
        private readonly Mock<IPacienteRepository> _repositoryMock;
        private readonly IMapper _mapper;
        private readonly PacienteService _service;

        public PacienteServiceTests()
        {
            _repositoryMock = new Mock<IPacienteRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreatePacienteDTO, Paciente>();
                cfg.CreateMap<Paciente, PacienteDTO>();
            });

            _mapper = config.CreateMapper();
            _service = new PacienteService(_repositoryMock.Object, _mapper); 
        }

        [Fact]
        public async Task AdicionarAsync_DeveRetornarPacienteDTO()
        {
            var dto = new CreatePacienteDTO { Nome = "Bruno", CPF = "123", Endereco = "Rua A", DataNascimento = DateTime.Now };
            _repositoryMock.Setup(r => r.AdicionarAsync(It.IsAny<Paciente>())).Returns(Task.CompletedTask);

            var resultado = await _service.AdicionarAsync(dto);

            Assert.NotNull(resultado);
            Assert.Equal(dto.Nome, resultado.Nome);
        }

        [Fact]
        public async Task ListarTodosAsync_DeveRetornarListaDePacienteDTO()
        {
            var pacientes = new List<Paciente>
            {
                new Paciente { Id = 1, Nome = "João", CPF = "111", Endereco = "Rua X", DataNascimento = DateTime.Now }
            };

            _repositoryMock.Setup(r => r.ListarTodosAsync()).ReturnsAsync(pacientes);

            var resultado = await _service.ListarTodosAsync();

            Assert.Single(resultado);
            Assert.Equal("João", resultado.First().Nome);
        }

        [Fact]
        public async Task BuscarPorIdAsync_DeveRetornarPacienteDTO_QuandoEncontrado()
        {
            var paciente = new Paciente { Id = 1, Nome = "Maria", CPF = "222", Endereco = "Rua Y", DataNascimento = DateTime.Now };
            _repositoryMock.Setup(r => r.BuscarPorIdAsync(1)).ReturnsAsync(paciente);

            var resultado = await _service.BuscarPorIdAsync(1);

            Assert.NotNull(resultado);
            Assert.Equal("Maria", resultado?.Nome);
        }

        [Fact]
        public async Task BuscarPorIdAsync_DeveRetornarNull_QuandoNaoEncontrado()
        {
            _repositoryMock.Setup(r => r.BuscarPorIdAsync(It.IsAny<int>())).ReturnsAsync((Paciente?)null);

            var resultado = await _service.BuscarPorIdAsync(999);

            Assert.Null(resultado);
        }

         

        [Fact]
        public async Task AtualizarAsync_DeveRetornarFalse_QuandoPacienteNaoExiste()
        {
            _repositoryMock.Setup(r => r.BuscarPorIdAsync(99)).ReturnsAsync((Paciente?)null);

            var resultado = await _service.AtualizarAsync(99, new CreatePacienteDTO());

            Assert.False(resultado);
        }

        [Fact]
        public async Task DeletarAsync_DeveRetornarTrue_QuandoPacienteExiste()
        {
            var paciente = new Paciente { Id = 1 };

            _repositoryMock.Setup(r => r.BuscarPorIdAsync(1)).ReturnsAsync(paciente);
            _repositoryMock.Setup(r => r.DeletarAsync(It.IsAny<Paciente>())).Returns(Task.CompletedTask);

            var resultado = await _service.DeletarAsync(1);

            Assert.True(resultado);
        }

        [Fact]
        public async Task DeletarAsync_DeveRetornarFalse_QuandoPacienteNaoExiste()
        {
            _repositoryMock.Setup(r => r.BuscarPorIdAsync(123)).ReturnsAsync((Paciente?)null);

            var resultado = await _service.DeletarAsync(123);

            Assert.False(resultado);
        }
    }
}
