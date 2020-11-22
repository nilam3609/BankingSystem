using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineBanking.Domain.Dto;
using OnlineBanking.Service.Interface;
using System.Threading.Tasks;

namespace OnlineBanking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientService _clientService;

        public ClientController(ILogger<ClientController> logger, IClientService clientService)
        {
            _logger = logger;
            _clientService = clientService;
        }

        [HttpPost("CreateClient")]
        public async Task<ClientResponseDto> CreateClient(ClientRequestDto clientRequestDto)
        {
            var client = await _clientService.CreateClient(clientRequestDto);
            return client;
        }
    }
}