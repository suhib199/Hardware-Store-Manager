using HardwareStoreRepository.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HardwareStoreManagement.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class EmailController : ControllerBase
    {
        /// <summary>
        /// Send an email
        /// </summary> 
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Email/SendEmail
        ///     {        
        ///       "recipientEmail": "isabella.bauch@ethereal.email",
        ///       "subject": "API Project",
        ///       "body": "The project has been completed successfully."    
        ///     }
        /// </remarks>

        [HttpPost]
        public IActionResult SendEmail([FromBody] HardwareStoreRepository.DTO.Email.EmailDTOReq email)
        {
            string recipientEmail = email.RecipientEmail;
            string subject = email.Subject;
            string body = email.Body;
            EmailHelper.SendEmail(recipientEmail, subject, body);
            return Ok("Email sent successfully.");
        }
    }
}

