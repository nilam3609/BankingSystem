using OnlineBanking.Domain.Dto;
using OnlineBanking.Repository.Interface;
using OnlineBanking.Service.Interface;
using System.Threading.Tasks;

namespace OnlineBanking.Service
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        /// <summary>
        /// Create client
        /// </summary>
        /// <param name="clientDto"></param>
        /// <returns></returns>
        public async Task<ClientResponseDto> CreateClient(ClientRequestDto clientDto)
        {
            return await _clientRepository.CreateClient(clientDto);
        }
    }
}