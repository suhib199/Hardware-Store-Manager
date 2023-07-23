using HardwareStoreRepository.DTO.Employee;
using HardwareStoreRepository.Repo.Employee;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HardwareStoreManagement.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository ;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Retrieve all employees info from the product table
        /// </summary> 
        [HttpGet]
        [Route("GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            Log.Information("You tried to retrieve all empolyees information");

            try
            {
                var Employee = _employeeRepository.GetAllEmployees();
                Log.Information("The empolyees info has been successfully retrieved");
                return Ok(Employee);
            }
            catch (Exception ex)
            {
                EmployeeDTORes res = new EmployeeDTORes();
                res.ResponseCode = 500;
                res.ResponseMessage = ex.Message;
                Log.Information($"Error while using action: ({ex.Message})");
                return new ObjectResult(res) { StatusCode = 500 };

            }    
        }

        /// <summary>
        /// Retrieve a specific employee info by by ID
        /// </summary> 
        [HttpGet]
        [Route("GetEmployeeById", Name = nameof(GetEmployeeById))]
        public IActionResult GetEmployeeById(int id)
        {
            Log.Information("You tried to retrieve a specific empolyee info by ID");
            try
            {
                var Employee = _employeeRepository.GetEmployeeById(id);

                if (Employee == null)
                {
                    return NotFound();
                }
                Log.Information("The empolyee info has been successfully retrieved");
                return Ok(Employee);
            }
        
              catch (Exception ex)
            {
                EmployeeDTORes res = new EmployeeDTORes();
                res.ResponseCode = 500;
                res.ResponseMessage = ex.Message;
                Log.Information($"Error while using action: ({ex.Message})");
                return new ObjectResult(res) { StatusCode = 500 };

            }
        }

        /// <summary>
        /// Add new employee to the table
        /// </summary> 
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Employee/CreateEmployee
        ///     {        
        ///       "name": "Suhib",
        ///       "position": "CI"
        ///           
        ///     }
        /// </remarks>

        [HttpPost]
        [Route("CreateEmployee")]
        public IActionResult CreateEmployee([FromBody] HardwareStoreRepository.DTO.EmployeeDTOReq Employee)
        {
            Log.Information("You tried to add new empolyee");
            try
            {
                if (Employee == null)
                {
                    return BadRequest("Invoice data is missing.");
                }

                _employeeRepository.CreateEmployee(Employee);
                Log.Information("The empolyee has been added successfully to the table");
                return CreatedAtRoute(nameof(GetEmployeeById), new { id = Employee.Id }, Employee);
            }

            catch (Exception ex)
            {
                EmployeeDTORes res = new EmployeeDTORes();
                res.ResponseCode = 500;
                res.ResponseMessage = ex.Message;
                Log.Information($"Error while using action: ({ex.Message})");
                return new ObjectResult(res) { StatusCode = 500 };

            }
        }


        /// <summary>
        ///Update an existing employee
        /// </summary> 
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/Employee/UpdateEmployee
        ///     {        
        ///       "Id": 1,
        ///       "name": "Mohammd",
        ///       "position": "S.E"
        ///           
        ///     }
        /// </remarks>
        [HttpPut("UpdateEmployee")]
        public IActionResult UpdateEmployee(int id, [FromBody] HardwareStoreRepository.DTO.EmployeeDTOReq employee)
        {
            Log.Information("You tried to update  an employee info");

            try
            {
                _employeeRepository.UpdateEmployee(id, employee);
                Log.Information("The employee information has been updated successfully");
                return NoContent(); 
             }
             catch (ArgumentException ex)
             {
                EmployeeDTORes res = new EmployeeDTORes();
                res.ResponseCode = 500;
                res.ResponseMessage = ex.Message;
                Log.Information($"Error while using action: ({ex.Message})");
                return new ObjectResult(res) { StatusCode = 500 };
            }
       
        }


    /// <summary>
    /// Delete a specific employee using their ID number
    /// </summary> 
        [HttpDelete]
        [Route("DeleteEmployee")]
        public IActionResult DeleteEmployee(int id)
        {
            Log.Information("You tried to delete a specific empolyee");
            try
            {
                _employeeRepository.DeleteEmployee(id);
                Log.Information("The empolyee has been deleted successfully from the table");
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                EmployeeDTORes res = new EmployeeDTORes();
                res.ResponseCode = 500;
                res.ResponseMessage = ex.Message;
                Log.Information($"Error while using action: ({ex.Message})");
                return new ObjectResult(res) { StatusCode = 500 };
            }
        }
    }
}
