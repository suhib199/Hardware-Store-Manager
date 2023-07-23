using HardwareStoreRepository.DBContext;
using HardwareStoreRepository.Models;
using HardwareStoreRepository.Repo.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.Repo.Invoice
{
    public class InvoiceImplement : IInvoiceRepossitory
    {
        private readonly HardwareStoreDBContext _HardwareStoreDBContext;

        public InvoiceImplement(HardwareStoreDBContext HardwareStoreDBContext)
        {
            _HardwareStoreDBContext = HardwareStoreDBContext;
        }

        public IEnumerable<DTO.InvoiceDTOReq> GetAllInvoices()
        {
            var invoices = _HardwareStoreDBContext.Invoice
                .Include(i => i.Customer)  
                .Include(i => i.Employee) 
                .Select(i => new DTO.InvoiceDTOReq
                {
                    Id = i.Id,
                    CustomerId = i.CustomerId,
                    EmployeeId = i.EmployeeId,
                    Date = i.Date,
                    InvoiceNumber = i.InvoiceNumber,
                    TotalPrice = i.TotalPrice,
                })
                .ToList();

            return invoices;
        }
        //---------------------------------------GetInvoiceById-------------------------------------

        public DTO.InvoiceDTOReq GetInvoiceById(int id)
        {
            var invoice = _HardwareStoreDBContext.Invoice
                .Include(i => i.Customer)  
                .Include(i => i.Employee)  
                .FirstOrDefault(i => i.Id == id);

            if (invoice == null)
                return null;

            var invoiceDTO = new DTO.InvoiceDTOReq
            {
                Id = invoice.Id,
                CustomerId = invoice.Customer.Id,
                EmployeeId = invoice.Employee.Id,
                Date = invoice.Date,
                InvoiceNumber = invoice.InvoiceNumber,
                TotalPrice = invoice.TotalPrice
            };
            return invoiceDTO;
        }
        // -----------------------------------------CreateInvoice------------------------------
        public void CreateInvoice(DTO.InvoiceDTOReq invoice)
        {
            var newInvoice = new Models.Invoice
            {
                CustomerId = invoice.CustomerId,
                EmployeeId = invoice.EmployeeId,
                Date = invoice.Date,
                TotalPrice = invoice.TotalPrice,
                InvoiceNumber = invoice.InvoiceNumber,
            };

            _HardwareStoreDBContext.Invoice.Add(newInvoice);
            _HardwareStoreDBContext.SaveChanges();
            int newInvoiceId = newInvoice.Id;
        }
        //-----------------------------------UpdateInvoice--------------------------------------------

        public void UpdateInvoice(int id, DTO.InvoiceDTOReq invoiceDto)
        {
            var existingInvoice = _HardwareStoreDBContext.Invoice.Find(id);
            if (existingInvoice == null)
            {
                throw new ArgumentException("Invoice not found.");
            }
            existingInvoice.CustomerId = invoiceDto.CustomerId;
            existingInvoice.EmployeeId = invoiceDto.EmployeeId;
            existingInvoice.Date = invoiceDto.Date;
            existingInvoice.TotalPrice = invoiceDto.TotalPrice;
            existingInvoice.InvoiceNumber = invoiceDto.InvoiceNumber;

            _HardwareStoreDBContext.SaveChanges();
        }
        //--------------------------------------------DeleteInvoice----------------------------
        public void DeleteInvoice(int id)
        {
            var existingInvoice = _HardwareStoreDBContext.Invoice.Find(id);
            if (existingInvoice == null)
            {
                throw new ArgumentException("Invoice not found.");
            }

            _HardwareStoreDBContext.Invoice.Remove(existingInvoice);
            _HardwareStoreDBContext.SaveChanges();
        }

    }
}
