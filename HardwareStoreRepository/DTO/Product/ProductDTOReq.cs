using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.DTO
{
    public class ProductDTOReq
    {
       
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the product name")]
        [RegularExpression("^([A-Z][a-z]*\\s)*[A-Z][a-z]*$", ErrorMessage = "Each part of product name must be start with capital letter")]
        public string Name { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "The description is required")]
        public string Description { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is mandatory")]
        [RegularExpression("^\\d+(\\.\\d+)?$", ErrorMessage = "The price must consist of numbers only")]
        public decimal Price { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is mandatory")]
        public int BarcodeId { get; set; }
    }
}
