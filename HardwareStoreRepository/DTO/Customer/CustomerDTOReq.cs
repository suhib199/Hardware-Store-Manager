using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.DTO
{
    public class CustomerDTOReq
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Mandatory field")]
        [RegularExpression("^([A-Z][a-z]*\\s)*[A-Z][a-z]*$", ErrorMessage = "Each part of customer name must be start with capital letter")]
        public string Name { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is mandatory (To contact with you if it necessary) ")]
        [RegularExpression("^[0-9A-Za-z]{3,}((@hotmail.com)|(@yahoo.com)|(@gmail.com))", ErrorMessage = "Pleas enter a vailed email format")]
        public string Email { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is mandatory ")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "The Jordainan phone number consists of 10 digits")]
        [RegularExpression("^[07]{2}[7-9]{1}[0-9]{7}", ErrorMessage = "The phone number must starting with 079,078,or 077 and contains only 10 digits")]
        public string Phone { get; set; }
    }
}
