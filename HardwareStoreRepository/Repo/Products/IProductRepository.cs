using HardwareStoreRepository.DTO;
using HardwareStoreRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.Repo.Products
{
    public  interface IProductRepository
    {
        IEnumerable<DTO.ProductDTOReq> GetAllProducts();
        DTO.ProductDTOReq GetProductById(int id);
        void CreateProduct(DTO.ProductDTOReq product);
        void UpdateProduct(int id, DTO.ProductDTOReq product);
        void DeleteProduct(int id);
    }



}
