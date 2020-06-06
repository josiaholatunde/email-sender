using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using EmailSender.Helpers;
using EmailSender.Repositories;
using EmailSender.DTOS;
using System.Threading.Tasks;

namespace EmailSender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IEmailService _emailService;

        public EmailController(ILogger<EmailController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }


        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> SendMail([FromForm] EmailDTO emailDTO)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values);
                }
                await _emailService.SendEmail(emailDTO);
                return Ok(new ApiResponse{
                    Message="Successfully sent email",
                    Data=null,
                    Errors=null
                });
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An Error occurred while sending email \nMessage:{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}