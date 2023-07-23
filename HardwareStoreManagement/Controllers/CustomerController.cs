using HardwareStoreRepository.DTO;
using HardwareStoreRepository.DTO.Customer;
using HardwareStoreRepository.Helper.CacheService;
using HardwareStoreRepository.Repo.Customer;
using HardwareStoreRepository.Repo.Invoice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HardwareStoreManagement.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IRedisCacheService _redis;
        public CustomerController(ICustomerRepository customerRepository, IRedisCacheService redis)
        {
            _customerRepository = customerRepository;
            _redis = redis;
        }

        /// <summary>
        /// Retrieve all customers info from the product table
        /// </summary> 
        [HttpGet]
        [Route("GetAllCustomers")]
        public async Task <IActionResult>  GetAllCustomers()
        {
            Log.Information("You tried to retrieve all customer information");
            try
            {
                var Customer = _customerRepository.GetAllCustomers();
                Log.Information("The customers info has been successfully retrieved");
                return Ok(Customer);

            }
            catch (Exception ex)
            {
                CustomerDTORes res = new CustomerDTORes();
                res.ResponseCode = 500;
                res.ResponseMessage = ex.Message;
                Log.Information($"Error while using action: {ex.Message}");
                return new ObjectResult(res) { StatusCode = 500 };

            }
            
        }

        /// <summary>
        /// Retrieve a specific customer info by by ID
        /// </summary> 
        [HttpGet]
        [Route("GetCustomerById", Name = nameof(GetCustomerById))]
        public IActionResult GetCustomerById(int id)
        {
            Log.Information("You tried to retrieve a specific customer info by ID");

            try
            {
                var Customer = _customerRepository.GetCustomerById(id);

                if (Customer == null)
                {
                    return NotFound();
                }
                Log.Information("The customer info has been successfully retrieved");
                return Ok(Customer);
            }
            catch (Exception ex)
            {
                CustomerDTORes res = new CustomerDTORes();
                res.ResponseCode = 500;
                res.ResponseMessage = ex.Message;
                Log.Information($"Error while using action: {ex.Message}");
                return new ObjectResult(res) { StatusCode = 500 };

            }
         
        }

        /// <summary>
        /// Add new customer to the table
        /// </summary> 
        /// /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/customers/CreateCustomer
        ///     {       
        ///       "Name": "Suhib Alhanafi",
        ///       "Email": "Suhib@hotmail.com",
        ///       "Phone": "0784321769"        
        ///     }
        /// </remarks>
        [HttpPost]
        [Route("CreateCustomer")]
        public IActionResult CreateCustomer([FromBody] HardwareStoreRepository.DTO.CustomerDTOReq Customer)
        {
            Log.Information("You tried to add new customer");
            try
            {

                if (Customer == null)
                {
                    return BadRequest("Invoice data is missing.");
                }

                _customerRepository.CreateCustomer(Customer);

                var cache= _redis.SetData<string>("Tempkey", Customer.Name, DateTime.Now.AddMinutes(1));
                if(cache)
                {
                    Log.Information("The customer has been added successfully to the table");
                    return CreatedAtRoute(nameof(GetCustomerById), new { id = Customer.Id }, Customer);
                }
                return BadRequest ("Redis Error");
               
            }
            catch (Exception ex)
            {
                CustomerDTORes res = new CustomerDTORes();
                res.ResponseCode = 500;
                res.ResponseMessage = ex.Message;
                Log.Information($"Error while using action: {ex.Message}");
                return new ObjectResult(res) { StatusCode = 500 };


            }
        }



        /// <summary>
        /// Update an existing customer
        /// </summary> 
        /// /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/customers/UpdateCustomer
        ///     {    
        ///       "Id": 1,
        ///       "Name": "Radi Alhanafi",
        ///       "Email": "Radi@yahoo.com",
        ///       "Phone": "0784321444"        
        ///     }
        /// </remarks>
        [HttpPut("UpdateCustomer")]
        public IActionResult UpdateCustomer(int id, [FromBody] CustomerDTOReq customer)
        {
            try
            {
                _customerRepository.UpdateCustomer(id, customer);
                return NoContent(); 
            }

            catch (Exception ex)
            {
                CustomerDTORes res = new CustomerDTORes();
                res.ResponseCode = 500;
                res.ResponseMessage = ex.Message;
                Log.Information($"Error while using action: {ex.Message}");
                return new ObjectResult(res) { StatusCode = 500 };
            }
        }



        /// <summary>
        /// Delete a specific customer using their ID number
        /// </summary> 
        [HttpDelete]
        [Route("DeleteCustomer")]
        public IActionResult DeleteCustomer(int id)
        {
            Log.Information("You tried to delete a specific customer");

            try
            {
                _customerRepository.DeleteCustomer(id);
                Log.Information("The customer has been deleted successfully from the table");
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                CustomerDTORes res = new CustomerDTORes();
                res.ResponseCode = 500;
                res.ResponseMessage = ex.Message;
                Log.Information($"Error while using action: {ex.Message}");
                return new ObjectResult(res) { StatusCode = 500 };
            }
        }
    }
}

