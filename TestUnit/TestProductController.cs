using HardwareStoreManagement.Controllers;
using HardwareStoreRepository.DTO.Product;
using HardwareStoreRepository.Repo.Products;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestUnit
{
    public class TestProductController
    {
        private readonly ProductController _productController;
        public TestProductController(ProductController productController)
        {
            _productController = productController;
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void DeletePruduct(int Id)
        {
            var res = _productController.DeleteProduct(Id);
            Assert.IsType<NoContentResult>(res);
        }
    }
    
}
