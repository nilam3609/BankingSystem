using OnlineBanking.Domain.Dto;
using System.Threading.Tasks;

namespace OnlineBanking.Service.Interface
{
    public interface IClientService
    {
        Task<ClientResponseDto> CreateClient(ClientRequestDto clientDto);
    }
}