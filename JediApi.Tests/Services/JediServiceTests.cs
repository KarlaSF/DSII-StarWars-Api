using JediApi.Models;
using JediApi.Repositories;
using JediApi.Services;
using Moq;


namespace JediApi.Tests.Services
{
    public class JediServiceTests
    {
        // não mexer
        private readonly JediService _service;
        private readonly Mock<IJediRepository> _repositoryMock;

        public JediServiceTests()
        {
            // não mexer
            _repositoryMock = new Mock<IJediRepository>();
            _service = new JediService(_repositoryMock.Object);
        }


        [Fact]
        public async Task GetById_Success()
        {
            var jedi = new jedi { Id = 1, Name = "fulano", Strength = 100, Version = 1 };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(1));

            var result = await _service.GetByIdAsync(1);

            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            var result = await _service.GetByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll()
        {
            var jediMuitos = new List<jedi>{
            new jedi{Id = 1, Name = "fulano", Strength = 100, Version = 1 },
            new jedi{Id = 2, Name = "ciclano", Strength = 200, Version = 1 },
        };
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(jediMuitos);

            var result = await _service.GetAllAsync();

            Assert.Equal(2, result.Count);
        }
    }
}