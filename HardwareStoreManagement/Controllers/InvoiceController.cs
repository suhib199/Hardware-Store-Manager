using HardwareStoreRepository.DTO.Employee;
using HardwareStoreRepository.DTO.Invoice;
using HardwareStoreRepository.DTO.Product;
using HardwareStoreRepository.Repo.Invoice;
using HardwareStoreRepository.Repo.Products;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HardwareStoreManagement.Controllers
{
    [Route("api/invoices")]
    [ApiController]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceRepossitory _invoiceRepossitory;

        public InvoiceController(IInvoiceRepossitory invoiceRepossitory)
        {
            _invoiceRepossitory = invoiceRepossitory;
        }

        /// <summary>
        /// Retrieve all invoices info from the product table
        /// </summary> 
      
        [HttpGet]
        [Route("GetAllInvoices")]
        public IActionResult GetAllInvoices()
        {
            Log.Information("You tried to retrieve all invoices information");
            try
            {
                var invoices = _invoiceRepossitory.GetAllInvoices();
                Log.Information("The invoices info has been successfully retrieved");
                return Ok(invoices);
            }      
            
            catch (Exception ex)
            {
                invoiceDTORes res = new invoiceDTORes();
                res.ResponseCode = 500;
                res.ResponseMessage = ex.Message;
                Log.Information($"Error while using action: {ex.Message}");
                return new ObjectResult(res) { StatusCode = 500 };

            }
        }


        /// <summary>
        /// Retrieve a specific invoice info by by ID
        /// </summary>
        [HttpGet]
        [Route("invoices", Name = nameof(GetInvoiceById))]
        public IActionResult GetInvoiceById(int id)
        {
            Log.Information("You tried to retrieve a specific invoice info by ID");

            try
            {
                var invoice = _invoiceRepossitory.GetInvoiceById(id);

                if (invoice == null)
                {
                    return NotFound();
                }
                Log.Information("The invoice info has been successfully retrieved");
                return Ok(invoice);
            }
        
            catch (Exception ex)
            {
                invoiceDTORes res = new invoiceDTORes();
                res.ResponseCode = 500;
                res.ResponseMessage = ex.Message;
                Log.Information($"Error while using action: {ex.Message}");
                return new ObjectResult(res) { StatusCode = 500 };

            }
        }


        /// <summary>
        /// Add new invoice to the table
        /// </summary> 
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/invoice/CreateInvoice
        ///     {        
        ///        "customerId": 1,
        ///        "employeeId": 1,
        ///        "invoiceNumber": 1,
        ///         "totalPrice": 111,
        ///         "date": "2023-07-22T14:29:54.872Z"    
        ///     }
        /// </remarks>
        [HttpPost]
        [Route("invoices")]
        public IActionResult CreateInvoice([FromBody] HardwareStoreRepository.DTO.InvoiceDTOReq invoice)
        {
            Log.Information("You tried to add new Invoice");
            try
            {
                if (invoice == null)
                {
                    return BadRequest("Invoice data is missing.");
                }

                _invoiceRepossitory.CreateInvoice(invoice);
                Log.Information("The invoice has been added successfully to the table");
                return CreatedAtRoute(nameof(GetInvoiceById), new { id = invoice.Id }, invoice);
            }
        
            catch (Exception ex)
            {
                invoiceDTORes res = new invoiceDTORes();
                res.ResponseCode = 500;
                res.ResponseMessage = ex.Message;
                Log.Information($"Error while using action: {ex.Message}");
                return new ObjectResult(res) { StatusCode = 500 };

            }
        }


        /// <summary>
        /// Update an existing invoice
        /// </summary> 
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/invoice/UpdateInvoice
        ///     {      
        ///        "Id": 1,
        ///        "customerId": 1,
        ///        "employeeId": 1,
        ///        "invoiceNumber": 12,
        ///         "totalPrice": 22.3,
        ///         "date": "2023-07-22T14:29:54.872Z"    
        ///     }
        /// </remarks>
        [HttpPut("UpdateInvoice")]
        public IActionResult UpdateInvoice(int id, [FromBody] HardwareStoreRepository.DTO.InvoiceDTOReq invoice)
        {
            try
            {
                _invoiceRepossitory.UpdateInvoice(id, invoice);
                return NoContent(); 
            }
            catch (ArgumentException ex)
            {
                invoiceDTORes res = new invoiceDTORes();
                res.ResponseCode = 500;
                res.ResponseMessage = ex.Message;
                Log.Information($"Error while using action: {ex.Message}");
                return new ObjectResult(res) { StatusCode = 500 };
            }
            
        }



        /// <summary>
        /// Delete a specific invoice using their ID number
        /// </summary> 
        [HttpDelete]
        [Route("DeleteInvoice")]
        public IActionResult DeleteProduct(int id)
        {
            Log.Information("You tried to delete a specific Invoice");
            try
            {
                _invoiceRepossitory.DeleteInvoice(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                Log.Information($"Error while using action: {ex.Message}");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                invoiceDTORes res = new invoiceDTORes();
                res.ResponseCode = 500;
                res.ResponseMessage = ex.Message;
                Log.Information($"Error while using action: {ex.Message}");
                return new ObjectResult(res) { StatusCode = 500 };

            }
        }
    }
}

