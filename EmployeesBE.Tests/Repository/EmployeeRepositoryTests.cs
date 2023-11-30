using EmployeesBE.Data;
using EmployeesBE.Models;
using EmployeesBE.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EmployeesBE.Tests.Repository
{
    public class EmployeeRepositoryTests
    {
        private async Task<ApplicationDbContext> GetApplicationDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EmployeeDb")
                .Options;
            var dbContext = new ApplicationDbContext(options);
            await dbContext.Database.EnsureCreatedAsync();

            if (await dbContext.Employees.CountAsync() == 0)
            {
                dbContext.Employees.Add(new Employee()
                {
                    Id = 1,
                    Name = "Test",
                    LastName = "Test",
                });
                await dbContext.SaveChangesAsync();
            }

            return dbContext;
        }

        [Fact]
        public async void EmployeeRepository_CreateAsync_CreatesEmployee()
        {
            //Arrange
            var dbContext = await GetApplicationDbContext();
            var employeeRepository = new EmployeeRepository(dbContext);
            var employee = new Employee()
            {
                Id = 2,
                Name = "Test2",
                LastName = "Test2",
            };

            //Act
            await employeeRepository.CreateAsync(employee);

            //Assert
            var result = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == 2);
            result.Should().BeOfType<Employee>();
        }


        [Fact]
        public async Task EmployeeRepository_GetAllAsync_ReturnsEmployees()
        {
            //Arrange
            var dbContext = await GetApplicationDbContext();
            var employeeRepository = new EmployeeRepository(dbContext);

            //Act
            var result = await employeeRepository.GetAllAsync();

            //Assert
            result.Should().BeOfType<List<Employee>>();
        }

        [Fact]
        public async Task EmployeeRepository_GetByIdAsync_ReturnsEmployee()
        {
            //Arrange
            var id = 1;
            var dbContext = await GetApplicationDbContext();
            var employeeRepository = new EmployeeRepository(dbContext);

            //Act
            var result = await employeeRepository.GetByIdAsync(id);

            //Assert
            result.Should().BeOfType<Employee>();

        }


    }
}
