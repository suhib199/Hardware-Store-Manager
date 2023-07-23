using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.Repo.Employee
{
    public interface IEmployeeRepository
    {
        IEnumerable<DTO.EmployeeDTOReq> GetAllEmployees();
        DTO.EmployeeDTOReq GetEmployeeById(int id);
        void CreateEmployee(DTO.EmployeeDTOReq EmployeeDto);
        void UpdateEmployee(int id, DTO.EmployeeDTOReq EmployeeDto);
        void DeleteEmployee(int id);
    }
}
