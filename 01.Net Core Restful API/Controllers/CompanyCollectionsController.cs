using _01.Net_Core_Restful_API.Entities;
using _01.Net_Core_Restful_API.Helpers;
using _01.Net_Core_Restful_API.Models;
using _01.Net_Core_Restful_API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _01.Net_Core_Restful_API.Controllers
{
    [ApiController]
    //路由模板都小写
    [Route("api/companycollections")]
    public class CompanyCollectionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public CompanyCollectionsController(IMapper mapper, ICompanyRepository companyRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        }


        [HttpGet("{ids}", Name = nameof(GetCompanyCollection))]
        public async Task<IActionResult> GetCompanyCollection(
                [FromRoute]
                [ModelBinder(BinderType =typeof(ArrayModelBinder))]
                IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var entities = await _companyRepository.GetCompaniesAsync(ids);

            //ids的数量与转换后的实体数量要保持一致 
            if (ids.Count() != entities.Count())
            {
                return NotFound();
            }

            var dtosToReturn = _mapper.Map<IEnumerable<CompanyDto>>(entities);

            return Ok(dtosToReturn);

        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> CreateCompanyCollection(
            IEnumerable<CompanyAddDto> companyAddDtos)
        {
            if (companyAddDtos == null)
            {
                throw new ArgumentNullException(nameof(companyAddDtos));
            }

            var companyList = _mapper.Map<IEnumerable<Company>>(companyAddDtos);

            foreach (var item in companyList)
            {
                _companyRepository.AddCompany(item);
            }

            await _companyRepository.SaveAsync();

            var dtoToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyList);

            var idsString = string.Join(",", dtoToReturn.Select(x => x.Id));

            return CreatedAtRoute(nameof(GetCompanyCollection), new { ids = idsString }, dtoToReturn);

        }


    }
}
