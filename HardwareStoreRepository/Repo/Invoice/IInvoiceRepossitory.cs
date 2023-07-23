using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.Repo.Invoice
{
    public interface IInvoiceRepossitory
    {
        IEnumerable<DTO.InvoiceDTOReq> GetAllInvoices();
        DTO.InvoiceDTOReq GetInvoiceById(int id);
        void CreateInvoice(DTO.InvoiceDTOReq invoiceDto);
        void UpdateInvoice(int id, DTO.InvoiceDTOReq invoiceDto);
        void DeleteInvoice(int id);
    }
}
