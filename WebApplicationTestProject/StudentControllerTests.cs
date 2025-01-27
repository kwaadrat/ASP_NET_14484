using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApplication1.Controllers;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplicationTestProject
{
    public class StudentControllerTests
    {
        List<StudentViewModel> students;
        Mock<IStudentService> studentServiceMock;

        [SetUp]
        public void Setup()
        {
            // fill students model mock
            students = new List<StudentViewModel>();
            students.Add(new StudentViewModel() { Id = 1, Name = "Asterix", IndexNumber = "000001" });
            students.Add(new StudentViewModel() { Id = 2, Name = "Obelix", IndexNumber = "000002" });
            // create service mock
            studentServiceMock = new Mock<IStudentService>();
        }

        [Test]
        public void TestIndexAction()
        {
            // Arrange
            this.studentServiceMock.Setup(m => m.FindAll()).Returns(students);
            var studentController = new StudentController(studentServiceMock.Object);
            // Act
            var result = studentController.Index();
            // Assert
            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Index");
            Assert.IsNotNull(viewResult.Model);
            Assert.IsTrue(viewResult.Model is List<StudentViewModel>);
            var studentsModel = viewResult.Model as List<StudentViewModel>;
            Assert.That(studentsModel, Has.Count.EqualTo(2));
        }

        [Test]
        public void TestGetCreateAction()
        {
            // Arrange
            //this.studentServiceMock.Setup(m => m.Add()).Returns(5);
            var studentController = new StudentController(studentServiceMock.Object);

            // Act
            var result = studentController.Create();

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Create");
        }
        
        [Test]
        public void TestPostCreateActionPositive()
        {
            // Arrange
            StudentViewModel viewModel = new StudentViewModel();
            //this.studentServiceMock.Setup(m => m.Add(viewModel)).Returns(0);
            var studentController = new StudentController(studentServiceMock.Object);


            // Act
            var vm = new StudentViewModel() { IndexNumber = "", Name = "", Email="jankowalski@wp.pl" };
            var result = studentController.Create(vm);

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result is RedirectToActionResult);
        }
 
        
        [Test]
        public void TestPostCreateActionNegative()
        {
            // Arrange
            var studentController = new StudentController(studentServiceMock.Object);
            studentController.ModelState.AddModelError("fakeError", "fakeError");

            // Act
            var vm = new StudentViewModel() { };
            var result = studentController.Create(vm);

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
        }
        
    }
}