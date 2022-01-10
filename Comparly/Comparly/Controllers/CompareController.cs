using Comparly.Data.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comparly.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompareController : ControllerBase
    {
        public CompareController()
        {

        }

        [HttpPost]
        [Route("upload")]
        public IActionResult UploadFiles(List<IFormFile> formFiles,string student1, string student2)
        {

            return Ok();
        }
        [Route("compare-file")]
        public IActionResult CompareFile()
        {
            return Ok();
        }
        [HttpGet]
        [Route("get-comparison-history")]
        public IActionResult GetComparisonHistory()
        {
            return Ok();
        }
        [HttpGet]
        [Route("get-comparison-history-details")]
        public IActionResult GetComparisonHistoryDetails()
        {
            return Ok();
        }
    }
}
