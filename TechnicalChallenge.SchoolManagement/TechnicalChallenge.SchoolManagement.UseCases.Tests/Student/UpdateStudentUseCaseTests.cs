using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.Student;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;
using TechnicalChallenge.SchoolManagement.UseCases.Student;

namespace TechnicalChallenge.SchoolManagement.UseCases.Tests.Student
{
    public class UpdateStudentUseCaseTests
    {
        private readonly Mock<IRepository<Entities.Student>> _studentRepositoryMock;
        private readonly Mock<IMapper<UpdateStudentRequestDto, Entities.Student>> _mapperMock;
        private readonly UpdateStudentUseCase<UpdateStudentRequestDto> _updateStudentUseCase;

        public UpdateStudentUseCaseTests()
        {
            _studentRepositoryMock = new Mock<IRepository<Entities.Student>>();
            _mapperMock = new Mock<IMapper<UpdateStudentRequestDto, Entities.Student>>();
            _updateStudentUseCase = new UpdateStudentUseCase<UpdateStudentRequestDto>(_studentRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnSuccess_WhenStudentIsUpdated()
        {
            // Arrange
            var dtoInput = new UpdateStudentRequestDto { Id = 1, Name = "John", LastName = "Doe" };
            var student = new Entities.Student { Id = 1, Name = "John", LastName = "Doe" };

            _mapperMock.Setup(m => m.ToEntity(dtoInput)).Returns(student);
            _studentRepositoryMock.Setup(r => r.UpdateAsync(student)).ReturnsAsync(1);

            // Act
            var result = await _updateStudentUseCase.ExecuteAsync(dtoInput);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Data);
            Assert.Equal("Estudiante actualizado exitosamente", result.Message);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnError_WhenRepositoryReturnsZero()
        {
            // Arrange
            var dtoInput = new UpdateStudentRequestDto { Id = 1, Name = "John", LastName = "Doe" };
            var student = new Entities.Student { Id = 1, Name = "John", LastName = "Doe" };

            _mapperMock.Setup(m => m.ToEntity(dtoInput)).Returns(student);
            _studentRepositoryMock.Setup(r => r.UpdateAsync(student)).ReturnsAsync(0);

            // Act
            var result = await _updateStudentUseCase.ExecuteAsync(dtoInput);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Data);
            Assert.Contains(result.Errors, e => e.Message == "No se pudo actualizar el estudiante.");
        }

        [Fact]
        public async Task ExecuteAsync_ShouldHandleException_WhenMapperThrows()
        {
            // Arrange
            var dtoInput = new UpdateStudentRequestDto { Id = 1, Name = "John", LastName = "Doe" };

            _mapperMock.Setup(m => m.ToEntity(dtoInput)).Throws(new Exception("Mapping error"));

            // Act
            var result = await _updateStudentUseCase.ExecuteAsync(dtoInput);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Data);
            Assert.Contains(result.Errors, e => e.Message == "No se pudo actualizar el estudiante.");
        }

        [Fact]
        public async Task ExecuteAsync_ShouldHandleException_WhenRepositoryThrows()
        {
            // Arrange
            var dtoInput = new UpdateStudentRequestDto { Id = 1, Name = "John", LastName = "Doe" };
            var student = new Entities.Student { Id = 1, Name = "John", LastName = "Doe" };

            _mapperMock.Setup(m => m.ToEntity(dtoInput)).Returns(student);
            _studentRepositoryMock.Setup(r => r.UpdateAsync(student)).Throws(new Exception("Database error"));

            // Act
            var result = await _updateStudentUseCase.ExecuteAsync(dtoInput);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Data);
            Assert.Contains(result.Errors, e => e.Message == "No se pudo actualizar el estudiante.");
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnError_WhenInputIsInvalid()
        {
            // Arrange
            var invalidDtoInput = new UpdateStudentRequestDto { Id = 0, Name = "", LastName = "" };

            _mapperMock.Setup(m => m.ToEntity(invalidDtoInput)).Returns((Entities.Student)null);

            // Act
            var result = await _updateStudentUseCase.ExecuteAsync(invalidDtoInput);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Data);
            Assert.Contains(result.Errors, e => e.Message == "No se pudo actualizar el estudiante.");
        }
    }

}
