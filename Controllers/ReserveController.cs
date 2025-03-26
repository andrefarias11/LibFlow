using LibFlow.Dto.Reserve;
using LibFlow.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibFlow.Controllers
{
    [Route("api/reserve")]
    [ApiController]
    public class ReserveController : ControllerBase
    {
        private readonly RabbitMqService _rabbitMqService;
        public ReserveController(RabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        [HttpPost]
        public IActionResult ReserveBook([FromBody] ReserveBookDTO request)
        {
            if (string.IsNullOrEmpty(request.BookName) || string.IsNullOrEmpty(request.Email))
                return BadRequest("Book name and email are required.");

            _rabbitMqService.PublishMessage("book_reservation_queue", request);

            return Ok("Reservation request sent successfully!");
        }
    }
}
