using InterviewTest.Model;
using InterviewTest.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

// We are returning actual DB Entities, but we should be working with DTOs

namespace InterviewTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ListController : ControllerBase
    {

        private readonly IListService _listService;
        public ListController(IListService listService)
        {
            _listService = listService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get()
        {
            var nameValueList = await _listService.GetNamedValueListAsync();

            return Ok(nameValueList);


            /*var employees = new List<Employee>();

            var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var queryCmd = connection.CreateCommand();
                queryCmd.CommandText = @"SELECT Name, Value FROM Employees WHERE Name LIKE 'A%' OR Name LIKE 'B%' OR Name LIKE 'C%'";

                using (var reader = queryCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            Name = reader.GetString(0),
                            Value = reader.GetInt32(1)
                        });
                    }
                }

                return employees;
            }*/
        }

        [HttpPut]
        public async Task<IActionResult> Put(Employee employee)
        {
            var updatedEmployee = await _listService.UpdateEmployee(employee);

            if (updatedEmployee == null)
                return BadRequest();

            return Ok();



            /*   try
               {
                   var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };
                   using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
                   {
                       connection.Open();

                       // We use parametised queries to avoid SQL injection attacks
                       var queryCmd = connection.CreateCommand();

                       queryCmd.CommandText = @"UPDATE Employees SET Name = @name, Value = @value WHERE Id = @Id";
                       queryCmd.Parameters.AddWithValue("@Id", employee.Id);
                       queryCmd.Parameters.AddWithValue("@name", employee.Name);
                       queryCmd.Parameters.AddWithValue("@value", employee.Value);
                       queryCmd.ExecuteNonQuery();
                   }

                   // This function will increase all the values, but perhaps we would not want to update the employee we have modified
                   IncreaseEmployeesValues();

                   return Ok();
               }
               catch (Exception ex)
               {
                   return BadRequest(ex.Message);
               }*/

        }
        [HttpPost]
        public async Task<ActionResult<Employee>> Post(Employee employee)
        {
            var createdEmployee = await _listService.CreateEmployeeAsync(employee);

            if (createdEmployee == null)
                return BadRequest();

            return StatusCode((int)HttpStatusCode.Created, createdEmployee);

            /*try
            {
                var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };

                Employee newEmployee = new Employee();

                using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();

                    // We use parametised queries to avoid SQL injection attacks
                    var queryCmd = connection.CreateCommand();

                    queryCmd.CommandText = @"INSERT INTO Employees(name, value) VALUES (@name, @value)";
                    queryCmd.Parameters.AddWithValue("@name", employee.Name);
                    queryCmd.Parameters.AddWithValue("@value", employee.Value);

                    queryCmd.ExecuteNonQuery();

                    // We use parametised queries to avoid SQL injection attacks
                    var getIdCmd = connection.CreateCommand();

                    getIdCmd.CommandText = @"SELECT * FROM Employees ORDER BY Id DESC LIMIT 1";
                    var reader = getIdCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        newEmployee.Id = reader.GetInt32(0);
                        newEmployee.Name = reader.GetString(1);
                        newEmployee.Value = reader.GetInt32(2);
                    }


                }

                return StatusCode((int)HttpStatusCode.Created, newEmployee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }*/
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deletedResult = await _listService.DeleteEmployee(id);

            if (deletedResult == false)
                return BadRequest();

            return Ok();    


            /*try
            {
                var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };
                using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();

                    // We use parametised queries to avoid SQL injection attacks
                    var queryCmd = connection.CreateCommand();
                    queryCmd.CommandText = @"DELETE FROM Employees WHERE id = @id";
                    queryCmd.Parameters.AddWithValue("@id", id);

                    queryCmd.ExecuteNonQuery();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }*/
        }
    }
}
