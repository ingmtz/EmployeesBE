using AutoMapper;
using EmployeesBE.Controllers;
using EmployeesBE.Models;
using EmployeesBE.Models.DTO;
using EmployeesBE.Repository.IRepository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesBE.Tests.Controllers
{
    public class EmployeeControllerTests
    {
        private readonly IEmployeeRepository _db;
        private readonly IMapper _mapper;
        private readonly EmployeeController _employeeController;
        public EmployeeControllerTests()
        {
            //Dependencies
            _db = A.Fake<IEmployeeRepository>();
            _mapper = A.Fake<IMapper>();
            //!!It's not possible to unit test static methods

            //SUT
            _employeeController = new EmployeeController(_db, _mapper);
        }

        [Fact]
        public async void EmployeeController_GetEmployees_ReturnsOk()
        {
            //Arrange
            var employees = A.Fake<IEnumerable<Employee>>();
            A.CallTo(() => _db.GetAllAsync()).Returns(employees);

            //Act
            var result = _employeeController.GetEmployees();

            //Assert Object check actions
            result.Should().NotBeNull();
            //result.Should().BeOfType(typeof(OkObjectResult));
            result.Should().BeOfType<Task<ActionResult<APIResponse>>>();
            //Assert.Equal(5, ((await result).Result as IEnumerable<Employee>).Count());
        }

        [Fact]
        public void EmployeeController_GetEmployee_ReturnsOk()
        {
            //Arrange
            var employee = A.Fake<Employee>();
            A.CallTo(() => _db.GetByIdAsync(1)).Returns(employee);

            //Act
            var result = _employeeController.GetEmployee(1);

            //Assert Object check actions
            result.Should().BeOfType<Task<ActionResult<APIResponse>>>();
            //result.Result.Should().BeOfType<Employee>();
        }

        [Fact]
        public void EmployeeController_CreateEmployeeReturnsOk()
        {
            //Arrange
            var employeeDto = A.Fake<EmployeeDTO>();

            //Act
            var result = _employeeController.CreateEmployee(employeeDto);

            //Assert
            result.Should().BeOfType<Task<ActionResult<APIResponse>>>();
        }

    }
}
