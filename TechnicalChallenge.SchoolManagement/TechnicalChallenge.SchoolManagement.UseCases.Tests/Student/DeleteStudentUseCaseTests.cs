using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;
using TechnicalChallenge.SchoolManagement.UseCases.Student;

namespace TechnicalChallenge.SchoolManagement.UseCases.Tests.Student
{
    public class DeleteStudentUseCaseTests
    {
        private readonly Mock<IRepository<Entities.Student>> _studentRepositoryMock;
        private readonly DeleteStudentUseCase<Entities.Student> _deleteStudentUseCase;

        public DeleteStudentUseCaseTests()
        {
            _studentRepositoryMock = new Mock<IRepository<Entities.Student>>();
            _deleteStudentUseCase = new DeleteStudentUseCase<Entities.Student>(_studentRepositoryMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnSuccess_WhenStudentIsDeleted()
        {
            // Arrange
            int studentId = 1;

            _studentRepositoryMock.Setup(r => r.DeleteAsync(studentId)).ReturnsAsync(1);

            // Act
            var result = await _deleteStudentUseCase.ExecuteAsync(studentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Data);
            Assert.Equal("Estudiante eliminado exitosamente", result.Message);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnError_WhenStudentNotDeleted()
        {
            // Arrange
            int studentId = 1;

            _studentRepositoryMock.Setup(r => r.DeleteAsync(studentId)).ReturnsAsync(0);

            // Act
            var result = await _deleteStudentUseCase.ExecuteAsync(studentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Data);
            Assert.Contains(result.Errors, e => e.Message == $"No se pudo eliminar el estudiante con id {studentId}.");
        }

        [Fact]
        public async Task ExecuteAsync_ShouldHandleException_WhenRepositoryThrows()
        {
            // Arrange
            int studentId = 1;

            _studentRepositoryMock.Setup(r => r.DeleteAsync(studentId)).Throws(new Exception("Database error"));

            // Act
            var result = await _deleteStudentUseCase.ExecuteAsync(studentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Data);
            Assert.Contains(result.Errors, e => e.Message == $"Ocurrió un error al tratar de eliminar el estudiante con id {studentId}");
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnError_WhenIdIsInvalid()
        {
            // Arrange
            int invalidStudentId = -1;

            _studentRepositoryMock.Setup(r => r.DeleteAsync(invalidStudentId)).ReturnsAsync(0);

            // Act
            var result = await _deleteStudentUseCase.ExecuteAsync(invalidStudentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Data);
            Assert.Contains(result.Errors, e => e.Message == $"No se pudo eliminar el estudiante con id {invalidStudentId}.");
        }
    }
}
