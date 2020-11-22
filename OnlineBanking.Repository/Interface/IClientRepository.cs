using OnlineBanking.Domain.Dto;
using System.Threading.Tasks;

namespace OnlineBanking.Repository.Interface
{
    public interface IClientRepository
    {
        Task<ClientResponseDto> CreateClient(ClientRequestDto clientDto);
    }
}