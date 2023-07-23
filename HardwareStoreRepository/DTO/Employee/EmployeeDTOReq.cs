using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.DTO
{
    public class EmployeeDTOReq
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Mandatory field")]
        [RegularExpression("^([A-Z][a-z]*\\s)*[A-Z][a-z]*$", ErrorMessage = "Each part of employee name must be start with capital letter")]
        public string Name { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Mandatory field")]
        public string Position { get; set; }
    }
}
