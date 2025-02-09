using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Presenters.Student;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;
using TechnicalChallenge.SchoolManagement.UseCases.Student;

namespace TechnicalChallenge.SchoolManagement.UseCases.Tests.Student
{
    public class GetAllStudentUseCaseTests
    {
        private readonly Mock<IRepository<Entities.Student>> _studentRepositoryMock;
        private readonly Mock<IPresenter<Entities.Student, StudentViewModel>> _presenterMock;
        private readonly GetAllStudentUseCase<Entities.Student, StudentViewModel> _getAllStudentUseCase;

        public GetAllStudentUseCaseTests()
        {
            _studentRepositoryMock = new Mock<IRepository<Entities.Student>>();
            _presenterMock = new Mock<IPresenter<Entities.Student, StudentViewModel>>();
            _getAllStudentUseCase = new GetAllStudentUseCase<Entities.Student, StudentViewModel>(_studentRepositoryMock.Object, _presenterMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnStudents_WhenStudentsExist()
        {
            // Arrange
            var students = new List<Entities.Student>
            {
                new Entities.Student { Id = 1, Name = "John", LastName = "Doe" },
                new Entities.Student { Id = 2, Name = "Jane", LastName = "Smith" }
            };
            var studentViewModels = new List<StudentViewModel>
            {
                new StudentViewModel { Id = 1, Name = "John", LastName = "Doe" },
                new StudentViewModel { Id = 2, Name = "Jane", LastName = "Smith" }
            };

            _studentRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(students);
            _presenterMock.Setup(p => p.Present(students)).Returns(studentViewModels);

            // Act
            var result = await _getAllStudentUseCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Data);
            Assert.Equal(2, result.Data.Count());
            Assert.Equal("Estudiantes cargados exitosamente", result.Message);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnNoStudentsMessage_WhenNoStudentsExist()
        {
            // Arrange
            var students = new List<Entities.Student>();
            var studentViewModels = new List<StudentViewModel>();

            _studentRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(students);
            _presenterMock.Setup(p => p.Present(students)).Returns(studentViewModels);

            // Act
            var result = await _getAllStudentUseCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Data);
            Assert.Equal("No se encontraron estudiantes.", result.Message);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldHandleException_WhenRepositoryThrows()
        {
            // Arrange
            _studentRepositoryMock.Setup(r => r.GetAllAsync()).Throws(new Exception("Database error"));

            // Act
            var result = await _getAllStudentUseCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.Contains(result.Errors, e => e.Message == "Ocurrió un error al tratar de obtener los estudiantes");
        }

        [Fact]
        public async Task ExecuteAsync_ShouldHandleException_WhenPresenterThrows()
        {
            // Arrange
            var students = new List<Entities.Student>
            {
                new Entities.Student { Id = 1, Name = "John", LastName = "Doe" }
            };

            _studentRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(students);
            _presenterMock.Setup(p => p.Present(students)).Throws(new Exception("Presenter error"));

            // Act
            var result = await _getAllStudentUseCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.Contains(result.Errors, e => e.Message == "Ocurrió un error al tratar de obtener los estudiantes");
        }
    }
}

