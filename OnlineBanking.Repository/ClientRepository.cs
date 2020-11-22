using AutoMapper;
using OnlineBanking.Domain;
using OnlineBanking.Domain.Dto;
using OnlineBanking.Repository.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBanking.Repository
{
    /// <summary>
    /// Repository for client data
    /// </summary>
    public class ClientRepository : IClientRepository
    {
        private readonly BankContext _bankContext;
        private readonly IMapper _mapper;

        public ClientRepository(BankContext bankContext, IMapper mapper)
        {
            _bankContext = bankContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Create client for particular bank
        /// </summary>
        /// <param name="clientDto"></param>
        /// <returns></returns>
        public async Task<ClientResponseDto> CreateClient(ClientRequestDto clientDto)
        {
            try
            {
                var userEntity = _mapper.Map<User>(clientDto);
                var clientEntity = _mapper.Map<Client>(clientDto);
                var usernameExist = _bankContext.Users.FirstOrDefault(x => x.UserName == userEntity.UserName);
                if (usernameExist != null)
                {
                    throw new Exception("Username is already taken");
                }
                userEntity.CreatedDate = DateTime.Now;
                _bankContext.Add(userEntity);
                _bankContext.SaveChanges();

                clientEntity.UserId = userEntity.Id;
                clientEntity.CreatedDate = DateTime.Now;
                _bankContext.Add(clientEntity);
                _bankContext.SaveChanges();

                return new ClientResponseDto
                {
                    UserName = userEntity.UserName,
                    ClientId = clientEntity.Id,
                    UserId = userEntity.Id
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}