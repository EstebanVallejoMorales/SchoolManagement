using Moq;
using TechnicalChallenge.SchoolManagement.Dto.Student;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;
using TechnicalChallenge.SchoolManagement.UseCases.Student;

namespace TechnicalChallenge.SchoolManagement.UseCases.Tests.Student
{
    public class CreateStudentUseCaseTests
    {
        private readonly Mock<IRepository<Entities.Student>> _studentRepositoryMock;
        private readonly Mock<IMapper<CreateStudentRequestDto, Entities.Student>> _mapperMock;
        private readonly CreateStudentUseCase<CreateStudentRequestDto> _createStudentUseCase;

        public CreateStudentUseCaseTests()
        {
            _studentRepositoryMock = new Mock<IRepository<Entities.Student>>();
            _mapperMock = new Mock<IMapper<CreateStudentRequestDto, Entities.Student>>();
            _createStudentUseCase = new CreateStudentUseCase<CreateStudentRequestDto>(_studentRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnSuccess_WhenStudentIsCreated()
        {
            // Arrange
            var dtoInput = new CreateStudentRequestDto { Name = "John", LastName = "Doe" };
            var student = new Entities.Student { Id = 1, Name = "John", LastName = "Doe" };

            _mapperMock.Setup(m => m.ToEntity(dtoInput)).Returns(student);
            _studentRepositoryMock.Setup(r => r.AddAsync(student)).ReturnsAsync(1);

            // Act
            var result = await _createStudentUseCase.ExecuteAsync(dtoInput);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Data);
            Assert.Equal("Estudiante creado exitosamente", result.Message);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnError_WhenRepositoryReturnsZero()
        {
            // Arrange
            var dtoInput = new CreateStudentRequestDto { Name = "John", LastName = "Doe" };
            var student = new Entities.Student { Id = 1, Name = "John", LastName = "Doe" };

            _mapperMock.Setup(m => m.ToEntity(dtoInput)).Returns(student);
            _studentRepositoryMock.Setup(r => r.AddAsync(student)).ReturnsAsync(0);

            // Act
            var result = await _createStudentUseCase.ExecuteAsync(dtoInput);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Data);
            Assert.Contains(result.Errors, e => e.Message == "No se pudo crear el estudiante.");
        }

        [Fact]
        public async Task ExecuteAsync_ShouldHandleException_WhenMapperThrows()
        {
            // Arrange
            var dtoInput = new CreateStudentRequestDto { Name = "John", LastName = "Doe" };

            _mapperMock.Setup(m => m.ToEntity(dtoInput)).Throws(new Exception("Mapping error"));

            // Act
            var result = await _createStudentUseCase.ExecuteAsync(dtoInput);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Data);
            Assert.Contains(result.Errors, e => e.Message == "No se pudo crear el estudiante.");
        }

        [Fact]
        public async Task ExecuteAsync_ShouldHandleException_WhenRepositoryThrows()
        {
            // Arrange
            var dtoInput = new CreateStudentRequestDto { Name = "John", LastName = "Doe" };
            var student = new Entities.Student { Id = 1, Name = "John", LastName = "Doe" };

            _mapperMock.Setup(m => m.ToEntity(dtoInput)).Returns(student);
            _studentRepositoryMock.Setup(r => r.AddAsync(student)).Throws(new Exception("Database error"));

            // Act
            var result = await _createStudentUseCase.ExecuteAsync(dtoInput);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Data);
            Assert.Contains(result.Errors, e => e.Message == "No se pudo crear el estudiante.");
        }
    }
}