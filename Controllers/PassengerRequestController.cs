using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RideRequestService.Data;
using RideRequestService.Models;
using RideRequestService.RabbitMQ;
using RideRequestService.Repository;

namespace RideRequestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerRequestController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly RabbitMQPublisher _rabbitMQPublisher;
        private readonly IPassengerRequestRepository _passengerRequestRepository;

        public PassengerRequestController(IMediator mediator, RabbitMQPublisher rabbitMQPublisher, IPassengerRequestRepository passengerRequestRepository)
        {
            _mediator = mediator;
            _rabbitMQPublisher = rabbitMQPublisher;
            _passengerRequestRepository = passengerRequestRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddPassengerRequest([FromBody] PassengerRequest request)
        {
            await _mediator.Publish(new PassengerRequestNotification { PassengerRequest = request });
            _rabbitMQPublisher.PublishMessage(request, "PassengerRequestQueue");

            return Ok();
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptPassengerRequest([FromBody] PassengerRequest request)
        {
            await _passengerRequestRepository.AddPassengerRequest(request);
            return Ok();
        }
    }
}
