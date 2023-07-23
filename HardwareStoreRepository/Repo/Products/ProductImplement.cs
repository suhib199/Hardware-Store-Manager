using HardwareStoreRepository.DBContext;
using HardwareStoreRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.Repo.Products
{

    public class ProductImplement : IProductRepository
    {

        public readonly HardwareStoreDBContext _HardwareStoreDBContext;
        public ProductImplement(HardwareStoreDBContext HardwareStoreDBContext)
        {
            _HardwareStoreDBContext = HardwareStoreDBContext;
        }

        //----------------------------------------GetAllProducts-------------------------------
        public IEnumerable<DTO.ProductDTOReq> GetAllProducts()
        {
            var products = _HardwareStoreDBContext.Product.Select(pro => new DTO.ProductDTOReq
            {
                Id = pro.Id,
                Name = pro.Name,
                Description = pro.Description,
                Price = pro.Price,
                BarcodeId = pro.BarcodesId,

            }).ToList();

            return products;
        }


        //---------------------------------------GetProductById-----------------------------------
        public DTO.ProductDTOReq GetProductById(int id)
        {
            var product = _HardwareStoreDBContext.Product.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return null;
            }

            var productDto = new DTO.ProductDTOReq
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                BarcodeId = product.BarcodesId,

            };

            return productDto;
        }

        //-----------------------------------------------CreateNewProduct----------------------------------

        public void CreateProduct(DTO.ProductDTOReq product)
        {
            var products = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                BarcodesId = product.BarcodeId
            };
        }
        //---------------------------UpdateProduct---------------------------------------------------

        public void UpdateProduct(int id, DTO.ProductDTOReq product)
        {
            var existingProduct = _HardwareStoreDBContext.Product.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                throw new ArgumentException("Product not found.");
            }

            existingProduct.Name = product.Name ?? existingProduct.Name;
            existingProduct.Description = product.Description ?? existingProduct.Description;
            existingProduct.Price = product.Price > 0 ? product.Price : existingProduct.Price;
            _HardwareStoreDBContext.SaveChanges();
        }
        //----------------------------------DeleteSpecificProduct---------------------------------------------------------
        public void DeleteProduct(int id)
        {
            var product = _HardwareStoreDBContext.Product.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                _HardwareStoreDBContext.Product.Remove(product);
                _HardwareStoreDBContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Product not found.");
            }
        }
    }
}