using HardwareStoreRepository.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.Repo.Customer
{
    public class CustomerImplement:ICustomerRepository
    {
        private readonly HardwareStoreDBContext _HardwareStoreDBContext;

        public CustomerImplement(HardwareStoreDBContext HardwareStoreDBContext)
        {
            _HardwareStoreDBContext = HardwareStoreDBContext;
        }

        public IEnumerable<DTO.CustomerDTOReq> GetAllCustomers()
        {
            var custumers = _HardwareStoreDBContext.Customer
                .Select(i => new DTO.CustomerDTOReq
                {
                    Id = i.Id,
                    Name = i.Name,
                    Email = i.Email,
                    Phone = i.Phone,
                  
                })
                .ToList();

            return custumers;
        }
        //---------------------------------------GetCustomerById-------------------------------------

        public DTO.CustomerDTOReq GetCustomerById(int id)
        {
            var customer = _HardwareStoreDBContext.Customer.FirstOrDefault(p => p.Id == id);

            if (customer == null)
            {
                return null;
            }

            var customerDto = new DTO.CustomerDTOReq
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone

            };

            return customerDto;
        }
        // -----------------------------------------CreateCustomer------------------------------
        public void CreateCustomer(DTO.CustomerDTOReq customer)
        {
            var newCustomer = new Models.Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
            };

            _HardwareStoreDBContext.Customer.Add(newCustomer);
            _HardwareStoreDBContext.SaveChanges();
            int newCustomerId = newCustomer.Id;
        }
        //-----------------------------------UpdateCustomer--------------------------------------------

        public void UpdateCustomer(int id, DTO.CustomerDTOReq customer)
        {
            var existingInvoice = _HardwareStoreDBContext.Customer.Find(id);
            if (existingInvoice == null)
            {
                throw new ArgumentException("Invoice not found.");
            }
            existingInvoice.Id = customer.Id;
            existingInvoice.Name = customer.Name;
            existingInvoice.Phone = customer.Phone;
            existingInvoice.Email = customer.Email;

            _HardwareStoreDBContext.SaveChanges();
        }
        //--------------------------------------------DeleteCustomer-----------------------------------
        public void DeleteCustomer(int id)
        {
            var existingInvoice = _HardwareStoreDBContext.Customer.Find(id);
            if (existingInvoice == null)
            {
                throw new ArgumentException("Invoice not found.");
            }

            _HardwareStoreDBContext.Customer.Remove(existingInvoice);
            _HardwareStoreDBContext.SaveChanges();
        }
    }
}
