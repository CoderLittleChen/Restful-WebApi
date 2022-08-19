using _01.Net_Core_Restful_API.DtoParameter;
using _01.Net_Core_Restful_API.Entities;
using _01.Net_Core_Restful_API.Helpers;
using _01.Net_Core_Restful_API.Models;
using _01.Net_Core_Restful_API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace _01.Net_Core_Restful_API.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly IPropertyCheckerService _propertyCheckerService;

        public CompaniesController(
            ICompanyRepository companyRepository,
            IMapper mapper,
            IPropertyCheckerService propertyCheckerService)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
            _propertyCheckerService = propertyCheckerService ?? throw new ArgumentNullException(nameof(_propertyCheckerService));
        }


        /// <summary>
        /// 返回可以写Task<IActionResult> 或者  Task<CompanyDto>都可以 
        /// 最好的写法还是如下  在Swagger中返回值类型更明确
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetCompanies))]
        [HttpHead]
        public async Task<IActionResult> GetCompanies([FromQuery] CompanyDtoParameter parameters)
        {
            if (!_propertyCheckerService.TypeHasProperties<CompanyDto>(parameters.Fields))
            {
                return BadRequest();
            }

            var companies = await _companyRepository.GetCompaniesAsync(parameters);

            //var previousLink = companies.HasPrevious
            //    ? CreateCompaniesResourceUri(parameters, ResourceUriType.PreviousPage) : null;
            //var nextLink = companies.HasNext
            //    ? CreateCompaniesResourceUri(parameters, ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = companies.TotalCount,
                totalPage = companies.TotalPages,
                pageSize = companies.PageSize,
                currentPage = companies.CurrentPage,
                //previousLink,
                //nextLink
            };

            //添加自定义Header
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata,
                    new JsonSerializerOptions
                    {
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    }));

            //var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            //return Ok(companyDtos.ShapeData(parameters.Fields));

            var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            var shapeData = companyDtos.ShapeData(parameters.Fields);
            var links = CreateLinksForCompany(parameters, companies.HasPrevious, companies.HasNext);
            var shapeCompaniesWithLinks = shapeData.Select(c =>
            {
                var companyDict = c as IDictionary<string, object>;
                var companyLinks = CreateLinksForCompany((Guid)companyDict["Id"], null);
                companyDict.Add("links", companyLinks);
                return companyDict;
            });

            var linkedCollectionResource = new
            {
                values = shapeCompaniesWithLinks,
                links
            };

            return Ok(linkedCollectionResource);
        }

        [Produces("application/json",
                        "application/vnd.company.hateoas+json",
                                        "application/vnd.company.company.friendly+json",
                                        "application/vnd.company.company.friendly.hateoas+json",
                                        "application/vnd.company.company.full+json",
                                        "application/vnd.company.company.full.hateoas+json")]
        [HttpGet("{companyId}", Name = nameof(GetCompany))]
        //[Route("{companyId}")]
        public async Task<IActionResult> GetCompany(Guid companyId, string fields,
            [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parseMediaType))
            {
                return BadRequest();
            }

            if (!_propertyCheckerService.TypeHasProperties<CompanyDto>(fields))
            {
                return BadRequest();
            }

            var company = await _companyRepository.GetCompanyAsync(companyId);
            if (company == null)
            {
                return NotFound();
            }

            var isIncludeLinks = parseMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
            IEnumerable<LinkDto> myLinks = new List<LinkDto>();

            if (isIncludeLinks)
            {
                myLinks = CreateLinksForCompany(companyId, fields);
            }

            var primaryMediaType = isIncludeLinks
                    ? parseMediaType.SubTypeWithoutSuffix.Substring(0, parseMediaType.SubTypeWithoutSuffix.Length - 8)
                    : parseMediaType.SubTypeWithoutSuffix;

            if (primaryMediaType == "vnd.company.company.full")
            {
                var full = _mapper.Map<CompanyFullDto>(company)
                            .ShapeData(fields) as IDictionary<string, object>;
                if (isIncludeLinks)
                {
                    full.Add("links", myLinks);
                }

                return Ok(full);
            }

            var friendly = _mapper.Map<CompanyDto>(company)
                    .ShapeData(fields) as IDictionary<string, object>;

            if (isIncludeLinks)
            {
                friendly.Add("links", myLinks);
            }
            return Ok(friendly);


            //if (parseMediaType.MediaType == "application/vnd.company.hateoas+json")
            //{
            //    var links = CreateLinksForCompany(companyId, fields);
            //    var linkedDict = _mapper.Map<CompanyDto>(company).ShapeData(fields) as IDictionary<string, object>;
            //    linkedDict.Add("links", links);
            //    return Ok(linkedDict);
            //}
            //return Ok(_mapper.Map<CompanyDto>(company).ShapeData(fields));

        }


        [HttpPost(Name = nameof(CreateCompany))]
        public async Task<ActionResult<CompanyDto>> CreateCompany(CompanyAddDto companyAddDto)
        {
            //可以不写
            //if (company == null)
            //{
            //    return BadRequest();
            //}

            //将AddDto转换秤Dto  
            //Map方法  前边是Destination  后边是Source
            var company = _mapper.Map<Company>(companyAddDto);
            _companyRepository.AddCompany(company);
            await _companyRepository.SaveAsync();
            string name = nameof(GetCompanies);

            //创建资源之后  需要返回资源的唯一标识符  状态码201 Created
            //将创建的实体转成CompantDto 用于数据查询

            var returnDto = _mapper.Map<CompanyDto>(company);

            var links = CreateLinksForCompany(returnDto.Id, null);
            var linkedDict = returnDto.ShapeData(null) as IDictionary<string, object>;
            linkedDict.Add("links", links);

            CreatedAtRouteResult createdAtRouteResult = CreatedAtRoute(nameof(GetCompany),
                new { companyId = linkedDict["Id"] }, linkedDict);
            return createdAtRouteResult;


            //如果这里想要返回新创建的Conpany下的所有的Employee集合   怎么写？ 报错  routeValue为空？
            //var returnEmployeoListDto = _mapper.Map<ICollection<EmployeeDto>>(company.Employees);
            //CreatedAtRouteResult createdAtRouteResult = CreatedAtRoute("{companyId}/employees", new { companyId = company.Id, employeeAddDto = companyAddDto.Employees }, returnEmployeoListDto);
            //return createdAtRouteResult;

        }


        [HttpDelete("{companyId}", Name = nameof(DeleteCompany))]
        public async Task<ActionResult<CompanyDto>> DeleteCompany(Guid companyId)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            //找到这个Company实体
            var companyEntity = await _companyRepository.GetCompanyAsync(companyId);
            //这里如果想在删除Company的同时级联删除Employee   那么需要先把当前Company下的全部Employee查询出来 
            //添加到内存跟踪中
            var employees = await _companyRepository.GetEmployeesAsync(companyId, null);

            if (companyId == null)
            {
                return NotFound();
            }

            _companyRepository.DeleteCompany(companyEntity);

            await _companyRepository.SaveAsync();

            return NoContent();
        }


        [HttpOptions]
        public IActionResult GetCompaniesOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,OPTIONS");
            return Ok();
        }


        /// <summary>
        /// 分页查询数据  创建上一页 下一页的链接
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string CreateCompaniesResourceUri(CompanyDtoParameter parameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link(nameof(GetCompanies), new
                    {
                        fields = parameters.Fields,
                        pageNumber = parameters.PageNumber - 1,
                        pageSize = parameters.PageSize,
                        companyName = parameters.CompanyName,
                        searchTerm = parameters.SearchTerm
                    });
                case ResourceUriType.NextPage:
                    return Url.Link(nameof(GetCompanies), new
                    {
                        fields = parameters.Fields,
                        pageNumber = parameters.PageNumber + 1,
                        pageSize = parameters.PageSize,
                        companyName = parameters.CompanyName,
                        searchTerm = parameters.SearchTerm
                    });
                case ResourceUriType.CurrentPage:
                default:
                    return Url.Link(nameof(GetCompanies), new
                    {
                        fields = parameters.Fields,
                        pageNumber = parameters.PageNumber,
                        pageSize = parameters.PageSize,
                        companyName = parameters.CompanyName,
                        searchTerm = parameters.SearchTerm
                    });
            }
        }


        /// <summary>
        /// 单个对象满足HATEOAS
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        private IEnumerable<LinkDto> CreateLinksForCompany(Guid companyId, string fields)
        {
            var links = new List<LinkDto>();

            if (string.IsNullOrEmpty(fields))
            {
                links.Add(new LinkDto(Url.Link(nameof(GetCompany), new { companyId }), "self", "GET"));
            }
            else
            {
                links.Add(new LinkDto(Url.Link(nameof(GetCompany), new { companyId, fields }), "self", "GET"));
            }

            links.Add(new LinkDto(Url.Link(nameof(DeleteCompany), new { companyId }), "DeleteCompany", "DELETE"));

            links.Add(new LinkDto(Url.Link(nameof(EmployeeController.CreateEmployeeForCompany), new { companyId }),
                            "create_employee_for_company", "POST"));

            links.Add(new LinkDto(Url.Link(nameof(EmployeeController.GetEmployeesForCompany), new { companyId }),
                "employees", "GET"));

            return links;
        }


        private IEnumerable<LinkDto> CreateLinksForCompany(CompanyDtoParameter parameter, bool hasPrevious, bool hasNext)
        {
            var links = new List<LinkDto>();

            links.Add(new LinkDto(CreateCompaniesResourceUri(parameter, ResourceUriType.CurrentPage),
                    "self", "GET"));

            if (hasPrevious)
            {
                links.Add(new LinkDto(CreateCompaniesResourceUri(parameter, ResourceUriType.PreviousPage),
                   "previous_page", "GET"));
            }

            if (hasNext)
            {
                links.Add(new LinkDto(CreateCompaniesResourceUri(parameter, ResourceUriType.NextPage),
                   "next_page", "GET"));
            }

            return links;
        }


    }
}
