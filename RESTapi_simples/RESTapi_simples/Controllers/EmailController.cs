using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using MimeKit.Text;
using MailKit.Security;

namespace RESTapi_simples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService) 
        {
        
            _emailService = emailService;
        
        }

        [HttpPost]

        public IActionResult SendEmail(EmailDto request)
        {

            _emailService.SendEmail(request);




            return Ok();

        }
    }

}