using AutoMapper;
using MedTrackAPI.DTOs;
using MedTrackAPI.Models;
using MedTrackAPI.Repositories.Interfaces;
using MedTrackAPI.Services.Implementations;
using Moq;
using Xunit;

namespace MedTrackApi.Tests.ServicesTest
{
    public class MedicoServiceTest
    {
        private readonly Mock<IMedicoRepository> _repositoryMock;
        private readonly IMapper _mapper;
        private readonly MedicoService _service;

        public MedicoServiceTest()
        {
            _repositoryMock = new Mock<IMedicoRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateMedicoDTO, Medico>();
                cfg.CreateMap<Medico, MedicoDTO>();
            });

            _mapper = config.CreateMapper();
            _service = new MedicoService(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task AdicionarAsync_DeveRetornarMedicoDTO()
        {
            var dto = new CreateMedicoDTO { Nome = "Bruno", CRM = "M", Especialidade = "Cardiologista" };
            _repositoryMock.Setup(r => r.AdicionarAsync(It.IsAny<Medico>())).Returns(Task.CompletedTask);

            var resultado = await _service.AdicionarAsync(dto);

            Assert.NotNull(resultado);
            Assert.Equal(dto.Nome, resultado.Nome);
        }

        [Fact]
        public async Task ListarTodosAsync_DeveRetornarListaDeMedicoDTO()
        {
            var medicos = new List<Medico>
            {
                new Medico { Id = 1, nome = "Bruno", crm = "C", especialidade = "Cardiologista" }
            };

            _repositoryMock.Setup(m => m.ListarTodosAsync()).ReturnsAsync(medicos);

            var resultado = await _service.ListarTodosAsync();

            Assert.Single(resultado);
            Assert.Equal("Bruno", resultado.First().Nome);
        }

        [Fact]
        public async Task BuscarPorIdAsync_DeveRetornarMedicoDTO_QuandoEncontrado()
        {
            var medico = new Medico { Id = 1, nome = "Bruno", crm = "M", especialidade = "Cardiologista" };

            _repositoryMock.Setup(r => r.BuscarPorIdAsync(1)).ReturnsAsync(medico);

            var resultado = await _service.BuscarPorIdAsync(1);

            Assert.NotNull(resultado);
            Assert.Equal("Bruno", resultado?.Nome);
        }

        [Fact]
        public async Task BuscarPorIdAsync_DeveRetornarNull_QuandoNaoEncontrado()
        {
            _repositoryMock.Setup(r => r.BuscarPorIdAsync(It.IsAny<int>())).ReturnsAsync((Medico?)null);

            var resultado = await _service.BuscarPorIdAsync(999);

            Assert.Null(resultado);
        }

        [Fact]
        public async Task AtualizarAsync_DeveRetornarTrue_QuandoMedicoExiste()
        {
            var dto = new CreateMedicoDTO { Nome = "Carlos", CRM = "A", Especialidade = "Ortopedista" };
            var medicoExistente = new Medico { Id = 1 };

            _repositoryMock.Setup(r => r.ExisteAsync(1)).ReturnsAsync(true);

            _repositoryMock.Setup(r => r.AtualizarAsync(It.IsAny<Medico>())).Returns(Task.CompletedTask);

            var resultado = await _service.AtualizarAsync(1, dto);

            Assert.True(resultado);
        }


        [Fact]
        public async Task AtualizarAsync_DeveRetornarFalse_QuandoMedicoNaoExiste()
        {
            _repositoryMock.Setup(r => r.BuscarPorIdAsync(99)).ReturnsAsync((Medico?)null);

            var resultado = await _service.AtualizarAsync(99, new CreateMedicoDTO());

            Assert.False(resultado);
        }

        [Fact]
        public async Task DeletarAsync_DeveRetornarTrue_QuandoMedicoExiste()
        {
            var medico = new Medico { Id = 1 };

            _repositoryMock.Setup(r => r.BuscarPorIdAsync(1)).ReturnsAsync(medico);
            _repositoryMock.Setup(r => r.DeletarAsync(It.IsAny<Medico>())).Returns(Task.CompletedTask);

            var resultado = await _service.DeletarAsync(1);

            Assert.True(resultado);
        }

        [Fact]
        public async Task DeletarAsync_DeveRetornarFalse_QuandoMedicoNaoExiste()
        {
            _repositoryMock.Setup(r => r.BuscarPorIdAsync(123)).ReturnsAsync((Medico?)null);

            var resultado = await _service.DeletarAsync(123);

            Assert.False(resultado);
        }
    }
}
