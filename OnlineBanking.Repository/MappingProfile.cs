using AutoMapper;
using OnlineBanking.Domain;
using OnlineBanking.Domain.Dto;

namespace OnlineBanking.Repository
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<ClientRequestDto, User>();
            CreateMap<ClientRequestDto, Client>();
            CreateMap<AccountCreationRequestDto, Account>();
            CreateMap<AccountCreationRequestDto, DepositAccountSetting>();
            CreateMap<AnnualInterest, AccountSettingsDto>();
        }
    }
}