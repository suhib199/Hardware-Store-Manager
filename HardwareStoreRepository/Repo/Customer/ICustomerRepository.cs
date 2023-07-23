using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.Repo.Customer
{
    public interface ICustomerRepository
    {
        IEnumerable<DTO.CustomerDTOReq> GetAllCustomers();
        DTO.CustomerDTOReq GetCustomerById(int id);
        void CreateCustomer(DTO.CustomerDTOReq invoiceDto);
        void UpdateCustomer(int id, DTO.CustomerDTOReq invoiceDto);
        void DeleteCustomer(int id);
    }
}
