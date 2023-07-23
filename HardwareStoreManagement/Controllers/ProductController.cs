using HardwareStoreRepository.DBContext;
using HardwareStoreRepository.DTO;
using HardwareStoreRepository.DTO.Product;
using HardwareStoreRepository.Models;
using HardwareStoreRepository.Repo.Invoice;
using HardwareStoreRepository.Repo.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HardwareStoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _IProductRepository;
        public ProductController(IProductRepository IProductRepository)
        {
            this._IProductRepository = IProductRepository;
        }

        /// <summary>
        /// Retrieve all products info from the product table
        /// </summary>
        [HttpGet]
        [Route("GetProducts")]
        public IEnumerable<HardwareStoreRepository.DTO.ProductDTOReq> GetAllProducts(int? statusCode)
        {
            
                var products = _IProductRepository.GetAllProducts();
                return products;
                     
        }

        /// <summary>
        /// Retrieve a specific product info by by ID
        /// </summary>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                Log.Information("You tried to retrieve a specific customer info by ID");
                var product = _IProductRepository.GetProductById(id);

                if (product == null)
                {
                    return NotFound();
                }
                Log.Information("The product info has been successfully retrieved");
                return Ok(product);
            }
         
            catch (Exception ex)
            {
                ProductDTORes res = new ProductDTORes();
                res.ResponseCode = 500;
                res.ResponseMessage = ex.Message;
                Log.Information("Error while using action" + ex.Message);
                return new ObjectResult(res) { StatusCode = 500 };

            }
        }

        /// <summary>
        /// Add new product to the table
        /// </summary> 
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Product/AddProduct
        ///     {        
        ///        "Name": Mose,
        ///        "Description": Bluetooth Mouse,
        ///        "Price": 15,
        ///         "BarcodeId": 158121261278,   
        ///     }
        /// </remarks>
        [HttpPost]
        [Route("AddProduct")]
        public IActionResult AddProduct([FromBody] HardwareStoreRepository.DTO.ProductDTOReq product)
        {
            Log.Information("You tried to add new product");
            try
            {
                if (product == null)
                {
                    return BadRequest("Product data is missing.");
                }

                _IProductRepository.CreateProduct(product);
                Log.Information("The Product has been added successfully to the table");
                return StatusCode(201);
            }

            catch (Exception ex)
            {
                ProductDTORes res = new ProductDTORes();
                res.ResponseCode = 500;
                res.ResponseMessage = ex.Message;
                Log.Information("Error while using action" + ex.Message);
                return new ObjectResult(res) { StatusCode = 500 };

            }
        }

        /// <summary>
        /// Update an existing product
        /// </summary> 
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/Product/UpdateProduct
        ///     {        
        ///        "Id": 1,
        ///        "Name": Mouse,
        ///        "Description": Ultra Thin Wirless,
        ///        "Price": 47.8,
        ///         "BarcodeId": 125671278126,
        ///     }
        /// </remarks>
        [HttpPut]
        [Route("UpdateProduct")]
        public IActionResult UpdateProduct(int id, [FromBody] HardwareStoreRepository.DTO.ProductDTOReq product)
        {
            Log.Information("You tried to update  a product");
            try
            {
                _IProductRepository.UpdateProduct(id, product);
                Log.Information("The product has been updated successfully ");
                return NoContent();
            }

            catch (ArgumentException ex)
            {
                ProductDTORes res = new ProductDTORes();
                res.ResponseCode = 500;
                res.ResponseMessage = ex.Message;
                Log.Information($"Error while using action: ({ex.Message})");
                return new ObjectResult(res) { StatusCode = 500 };
            }
        }

        /// <summary>
        /// Delete a specific product using their ID number
        /// </summary> 
        [HttpDelete]
        [Route("DeleteProduct")]
        public IActionResult DeleteProduct(int id)
        {
            Log.Information("You tried to delete a specific product");

            try
            {
                _IProductRepository.DeleteProduct(id);
                Log.Information("The product has been deleted successfully from the table");
                return NoContent();
            }

            catch (ArgumentException ex)
            {
                ProductDTORes res = new ProductDTORes();
                res.ResponseCode = 500;
                res.ResponseMessage = ex.Message;
                Log.Information("Error while using action" + ex.Message);
                return new ObjectResult(res) { StatusCode = 500 };
            }
        }
    }


}
