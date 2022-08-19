using _01.Net_Core_Restful_API.DtoParameter;
using _01.Net_Core_Restful_API.Entities;
using _01.Net_Core_Restful_API.Models;
using _01.Net_Core_Restful_API.Services;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _01.Net_Core_Restful_API.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId}/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public EmployeeController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// 查询指定公司下的员工
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [HttpGet(Name =nameof(GetEmployeesForCompany))]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>>
            GetEmployeesForCompany(Guid companyId, [FromQuery] EmployeeDtoParameters parameters)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            EmployeeDtoParameters employeeDtoParameters = new EmployeeDtoParameters
            {
                Gender = parameters.Gender,
                Q = parameters.Q
            };

            var employees = await _companyRepository.GetEmployeesAsync(companyId, employeeDtoParameters);
            var employeesDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeesDtos);
        }

        [HttpGet("{employeeId}", Name = nameof(GetEmployeeForCompany))]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var employee = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return Ok(employeeDto);
        }


        [HttpPost(Name =nameof(CreateEmployeeForCompany))]
        public async Task<ActionResult<EmployeeDto>> CreateEmployeeForCompany(Guid companyId, EmployeeAddDto employeeAddDto)
        {
            //先判断CompanyId是否存在
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            //将AddDto转换成实体Dto
            var employee = _mapper.Map<Employee>(employeeAddDto);

            //添加
            _companyRepository.AddEmployee(companyId, employee);

            //保存
            await _companyRepository.SaveAsync();

            //将实体转成查询用的实体  EmployeeDto
            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            //返回新增资源唯一标识符
            return CreatedAtRoute(nameof(GetEmployeeForCompany), new
            {
                CompanyId = companyId,
                EmployeeId = employeeDto.Id
            }, employeeDto);
        }


        [HttpPut("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployeeForCompany(
            Guid companyId, Guid employeeId, EmployeeUpdateDto updateDto)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var employee = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employee == null)
            {
                //如果资源不存在 允许消费者来创建资源
                //将UpdateDto转换成保存数据的Employee实体
                var employeeDto = _mapper.Map<Employee>(updateDto);
                employeeDto.Id = employeeId;
                _companyRepository.AddEmployee(companyId, employeeDto);

                await _companyRepository.SaveAsync();

                //将实体转成查询用的实体  EmployeeDto
                var employeeShowDto = _mapper.Map<EmployeeDto>(employeeDto);

                //返回新增资源唯一标识符
                return CreatedAtRoute(nameof(GetEmployeeForCompany), new
                {
                    CompanyId = companyId,
                    EmployeeId = employeeShowDto.Id
                }, employeeShowDto);
            }

            //Source  Destination 
            Employee returnEmployee = _mapper.Map(updateDto, employee);
            _companyRepository.UpdateEmployee(employee);
            await _companyRepository.SaveAsync();

            //返回204
            return NoContent();

            //返回200 以及当前更新资源的uri
            //return CreatedAtRoute(nameof(GetEmployeeForCompany), new { companyId = companyId, employeeId = employee.Id }, null);

        }

        [HttpPatch]
        [Route("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> PartiallyUpdateEmployee(Guid companyId, Guid employeeId,
            JsonPatchDocument<EmployeeUpdateDto> patchDocument)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var employee = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employee == null)
            {
                //当指定资源未找到的时候 允许消费者新增资源
                var employeeUpdateDto = new EmployeeUpdateDto();

                //增加实体验证  由于这里是JsonPatchDocument类型  如果不加ModelState参数  不会执行实体类型的校验
                patchDocument.ApplyTo(employeeUpdateDto, ModelState);
                if (!TryValidateModel(employeeUpdateDto))
                {
                    return ValidationProblem(ModelState);
                }

                //将Update实体转换成Employee实体用于新增
                var employeeInitDto = _mapper.Map<Employee>(employeeUpdateDto);
                employeeInitDto.Id = employeeId;

                _companyRepository.AddEmployee(companyId, employeeInitDto);

                await _companyRepository.SaveAsync();

                var dtoReturn = _mapper.Map<EmployeeDto>(employeeInitDto);

                return CreatedAtRoute(nameof(GetEmployeeForCompany), new { companyId = companyId, employeeId = dtoReturn.Id }, dtoReturn);
            }

            //将Employee实体转换成UpdateDto
            var dtoToPatch = _mapper.Map<EmployeeUpdateDto>(employee);

            //需要处理验证错误 
            patchDocument.ApplyTo(dtoToPatch, ModelState);

            if (!TryValidateModel(dtoToPatch))
            {
                return ValidationProblem(ModelState);
            }

            //将dtoToPatch转换成Employee
            _mapper.Map(dtoToPatch, employee);

            _companyRepository.UpdateEmployee(employee);

            await _companyRepository.SaveAsync();

            return NoContent();


        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var employeeEntity = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employeeEntity == null)
            {
                return NotFound();
            }

            _companyRepository.DeleteEmployee(employeeEntity);
            await _companyRepository.SaveAsync();

            return NoContent();
        }



        public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }


    }
}
