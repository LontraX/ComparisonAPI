using Comparly.Data.Dtos;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> UploadFiles(List<IFormFile> formFiles,string student1, string student2)
        {

            return Ok();
        }

        [Route("compare-file")]
        [Authorize]
        public async Task<IActionResult> CompareFile()
        {
            return Ok();
        }

        [HttpGet]
        [Route("get-comparison-history")]
        [Authorize]
        public async Task<IActionResult> GetComparisonHistory()
        {
            return Ok();
        }

        [HttpGet]
        [Route("get-comparison-history-details")]
        [Authorize]
        public async Task<IActionResult> GetComparisonHistoryDetails()
        {
            return Ok();
        }
    }
}
