using InterviewTest.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewTest.Services
{
    public class ListService : IListService
    {
        private readonly InterviewTestDbContext _context;

        public ListService(InterviewTestDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetNamedValueListAsync()
        {
            var nameValuesList = await (from employees in _context.Employees
                                        where EF.Functions.Like(employees.Name, "A%")
                                        || EF.Functions.Like(employees.Name, "B%")
                                        || EF.Functions.Like(employees.Name, "C%")
                                        select new Employee
                                        {
                                            Name = employees.Name,
                                            Value = employees.Value
                                        }).ToListAsync();

            if (nameValuesList != null)
                return nameValuesList;

            return null;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            try
            {
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();

                return employee;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }


        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            var hasDeleted = false;

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
                hasDeleted = true;
            }

            return hasDeleted;
        }

        // EF Core magic: We pass in the employee and let it handle the rest
        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            _context.Entry<Employee>(employee).State = EntityState.Modified;

            try
            {
                await IncreaseEmployeesValues(employee.Id);
                await _context.SaveChangesAsync();
                return employee;
            }
            catch
            {
                return null;
            }
        }

        // Increment the field Value by 1 where the field Name begins with ‘E’ and by 10 where Name
        // begins with ‘G’ and all others by 100
        // Assumption: If *we* change the value of an Employee, this value should not be increased
        // So we take the Id of the Employee we just updated, to filter it out
        private async Task IncreaseEmployeesValues(int employeeIdNotToUpdate)
        {
            var employees = await _context.Employees.ToListAsync();

            foreach (var employee in employees)
            {
                if (employee.Id == employeeIdNotToUpdate)
                    continue;

                if (employee.Name.StartsWith('E'))
                    employee.Value++;
                else if (employee.Name.StartsWith('G'))
                    employee.Value = employee.Value + 10;
                else
                    employee.Value = employee.Value + 100;
            }

            // SQL Queries
            // UPDATE Employees SET Value = Value + 1 WHERE Name LIKE 'E%'
            // UPDATE Employees SET Value = Value + 10 WHERE Name LIKE 'G%'
            // UPDATE Employees SET Value = Value + 100 WHERE Name NOT LIKE 'E%' AND Name NOT LIKE 'G%'s
        }

    }
}
