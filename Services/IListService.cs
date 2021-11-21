using InterviewTest.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewTest.Services
{
    public interface IListService
    {
        public Task<List<Employee>> GetNamedValueListAsync();
        public Task<Employee> CreateEmployeeAsync(Employee employee);
        public Task<Employee> UpdateEmployee(Employee employee);
        public Task<bool> DeleteEmployee(int id);
    }
}
