using HardwareStoreRepository.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.Repo.Employee
{
    public class EmployeeImplement:IEmployeeRepository
    {
        private readonly HardwareStoreDBContext _HardwareStoreDBContext;

        public EmployeeImplement(HardwareStoreDBContext HardwareStoreDBContext)
        {
            _HardwareStoreDBContext = HardwareStoreDBContext;
        }

        public IEnumerable<DTO.EmployeeDTOReq> GetAllEmployees()
        {
            var Employees = _HardwareStoreDBContext.Employee
                .Select(i => new DTO.EmployeeDTOReq
                {
                    Id = i.Id,
                    Name = i.Name,
                    Position = i.Position                
                })
                .ToList();

            return Employees;
        }
        //---------------------------------------GetEmployeeById-------------------------------------

        public DTO.EmployeeDTOReq GetEmployeeById(int id)
        {
            var Employees = _HardwareStoreDBContext.Employee.FirstOrDefault(p => p.Id == id);

            if (Employees == null)
            {
                return null;
            }

            var EmployeesDto = new DTO.EmployeeDTOReq
            {
                Id = Employees.Id,
                Name = Employees.Name,
                Position = Employees.Position,

            };
            return EmployeesDto;
        }
        // -----------------------------------------CreateEmployee------------------------------
        public void CreateEmployee(DTO.EmployeeDTOReq Employee)
        {
            var newEmployee = new Models.Employee
            {
                Id = Employee.Id,
                Name = Employee.Name,
                Position = Employee.Position
            };

            _HardwareStoreDBContext.Employee.Add(newEmployee);
            _HardwareStoreDBContext.SaveChanges();
            int newCustomerId = newEmployee.Id;
        }
        //-----------------------------------UpdateEmployee--------------------------------------------

        public void UpdateEmployee(int id, DTO.EmployeeDTOReq Employee)
        {
            var existingInvoice = _HardwareStoreDBContext.Employee.Find(id);
            if (existingInvoice == null)
            {
                throw new ArgumentException("Invoice not found.");
            }
            existingInvoice.Id = Employee.Id;
            existingInvoice.Name = Employee.Name;
            existingInvoice.Position = Employee.Position;

            _HardwareStoreDBContext.SaveChanges();
        }
        //--------------------------------------------DeleteEmployee-----------------------------------
        public void DeleteEmployee(int id)
        {
            var existingEmployee = _HardwareStoreDBContext.Employee.Find(id);
            if (existingEmployee == null)
            {
                throw new ArgumentException("Invoice not found.");
            }

            _HardwareStoreDBContext.Employee.Remove(existingEmployee);
            _HardwareStoreDBContext.SaveChanges();
        }
    }
}

