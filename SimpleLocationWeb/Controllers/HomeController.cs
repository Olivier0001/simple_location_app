using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace SimpleLocationWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        public IActionResult SendMail(string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("janelle.bartoletti@ethereal.email"));
            email.To.Add(MailboxAddress.Parse("janelle.bartoletti@ethereal.email"));

            email.Subject = "Send mail in ASP.net core web api";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body};

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("janelle.bartoletti@ethereal.email", "nbtTkCJAhm6kvnprN7");
            smtp.Send(email);
            smtp.Disconnect(true);



            return Ok();
        }
    }
}
