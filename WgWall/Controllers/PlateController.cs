using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WgWall.Api.Dto;
using WgWall.Api.Request;
using WgWall.Controllers.Base;
using WgWall.Data.Model;
using WgWall.Data.Repository.Interfaces;

namespace WgWall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlateController : GetController<Plate, PlateDto, PlatePayload>
    {
        private readonly IFrontendUserRepository _frontendUserRepository;
        public PlateController(IPlateRepository entityRepository, IFrontendUserRepository frontendUserRepository) : base(entityRepository)
        {
            _frontendUserRepository = frontendUserRepository;
        }

        protected override async Task<bool> WriteIntoAsync(Plate target, PlatePayload source)
        {
            var frontendUser = await _frontendUserRepository.TryFindAsync(source.FrontendUserId);
            if (frontendUser == null)
            {
                return false;
            }

            target.Accountable = frontendUser;
            target.ValidityDate = DateTime.Today;
            target.DinnerState = source.DinnerState;

            return true;
        }

        protected override void GetMappingExpression(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Plate, PlateDto>().ForMember(plt => plt.FrontendUserId, opt => opt.MapFrom(d => d.Accountable.Id));
        }
    }
}
