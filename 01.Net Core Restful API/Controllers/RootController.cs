using _01.Net_Core_Restful_API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _01.Net_Core_Restful_API.Controllers
{
    [Route("api")]
    [ApiController]
    public class RootController : ControllerBase
    {
        public IActionResult GetRoot()
        {
            var links = new List<LinkDto>();
            links.Add(new LinkDto(Url.Link(nameof(GetRoot), new { }), "self", "GET"));
            links.Add(new LinkDto(Url.Link(nameof(CompaniesController.GetCompanies), new { }),
                "companies", "GET"));
            links.Add(new LinkDto(Url.Link(nameof(CompaniesController.GetCompanies), new { }),
                "create_company", "POST"));
            return Ok(links);
        }


    }
}
