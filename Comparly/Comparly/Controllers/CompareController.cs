using AutoMapper;
using Comparly.Data.Dtos;
using Comparly.Data.Models;
using Comparly.Data.Services.CloudinaryService.Interface;
using Comparly.Data.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly ICompareService _compareService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICloudinaryService _cloudinaryService;
        public int _perPage = 5;

        public CompareController(ICompareService compareService, IMapper mapper, UserManager<AppUser> userManager, ICloudinaryService cloudinaryService)
        {
            _compareService = compareService;
            _mapper = mapper;
            _userManager = userManager;
            _cloudinaryService = cloudinaryService;
            
        }

        [HttpPost]
        [Route("upload")]
        //[Authorize]
        public async Task<IActionResult> UploadFiles(List<IFormFile> formFiles,string student1, string student2)
        {
            Submission submission = new Submission()
            {
                FirstStudentsName = student1,
                SecondStudentsName = student2,
            };
            var uploadResponse1 = await _cloudinaryService.UploadFile(formFiles[0]);
            var uploadResponse2 = await _cloudinaryService.UploadFile(formFiles[1]);
            submission.DocumentAUrl = uploadResponse1.FileUrl;
            submission.DocumentBUrl = uploadResponse2.FileUrl;
            return Ok();
        }

        [Route("compare-file")]
        [Authorize]
        public async Task<IActionResult> CompareFile()
        {
            return Ok();
        }

        [HttpGet]
        [Route("get-comparison-history/{id}")]
        [Authorize]
        public async Task<IActionResult> GetComparisonHistoryById(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("invalid request");
            }

            var submission = await _compareService.GetSubmissionByIdAsync(id);
            if (submission == null)
            {
                ModelState.AddModelError("Submission", "Submission does not exist");
                return NotFound(ModelState);
            }

            ComparisonHistoryDto comparisonHistoryDto = _mapper.Map<ComparisonHistoryDto>(submission);
           
            return Ok(comparisonHistoryDto);
        }

        [HttpGet]
        [Route("get-comparison-history-by-user/{email}")]
        [Authorize]
        public async Task<IActionResult> GetComparisonHistoryByUser(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("invalid request");
            }

            AppUser checkUser = await _userManager.FindByEmailAsync(email);
            if (checkUser != null)
            {
                ModelState.AddModelError("model", "User does not exist");
                return BadRequest(ModelState);
            }

            var submission = await _compareService.GetAllSubmissionsByUser(checkUser);
            if (submission == null)
            {
                ModelState.AddModelError("Submission", "Submission does not exist");
                return NotFound(ModelState);
            }

            ComparisonHistoryDto comparisonHistoryDto = _mapper.Map<ComparisonHistoryDto>(submission);

            return Ok(comparisonHistoryDto);
            
        }

        [HttpGet]
        [Route("get-all-comparison-history/{page}")]
        public async Task<IActionResult> GetAllComparisonHistory(int page)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("invalid request");
            }

            page = page <= 0 ? 1 : page;

            var submissions = await _compareService.GetAllSubmissionsAsync(page, _perPage);
            var comparisonToReturn = new List<ComparisonHistoryDto>();

            foreach (var item in submissions)
            {
                var itemsToBeAdded = new ComparisonHistoryDto
                {
                    FirstStudentsName = item.FirstStudentsName,
                    SecondStudentsName = item.SecondStudentsName,
                    DocumentAUrl = item.DocumentAUrl,
                    DocumentBUrl = item.DocumentBUrl
                };
                comparisonToReturn.Add(itemsToBeAdded);
            }

            return Ok(comparisonToReturn);
        }

        [HttpGet]
        [Route("get-comparison-history-details/{page}")]
        [Authorize]
        public async Task<IActionResult> GetComparisonHistoryDetails(int page)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("invalid request");
            }

            page = page <= 0 ? 1 : page;

            var submissions = await _compareService.GetAllSubmissionsAsync(page, _perPage);
            var comparisonDetailsToReturn = new List<ComparisonHistoryDetailsDto>();

            foreach (var item in submissions)
            {
                var itemsToBeAdded = new ComparisonHistoryDetailsDto
                {
                    FirstStudentsName = item.FirstStudentsName,
                    SecondStudentsName = item.SecondStudentsName,
                    DocumentAUrl = item.DocumentAUrl,
                    DocumentBUrl = item.DocumentBUrl,
                    SubmittedAt = item.SubmittedAt,
                    PercentageSimilarity = item.PercentageSimilarity,
                    SubmittedBy = item.SubmittedBy,
                    TextExplanation = item.TextExplanation
                };
                comparisonDetailsToReturn.Add(itemsToBeAdded);
            }

            return Ok(comparisonDetailsToReturn);
        }

        [HttpGet]
        [Route("get-comparison-history-details-by-user/{email}")]
        [Authorize]
        public async Task<IActionResult> GetComparisonHistoryDetailsByUser(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("invalid request");
            }

            AppUser checkUser = await _userManager.FindByEmailAsync(email);
            if (checkUser != null)
            {
                ModelState.AddModelError("model", "User does not exist");
                return BadRequest(ModelState);
            }

            var submissions = await _compareService.GetAllSubmissionsByUser(checkUser);
            var comparisonDetailsToReturn = new List<ComparisonHistoryDetailsDto>();

            foreach (var item in submissions)
            {
                var itemsToBeAdded = new ComparisonHistoryDetailsDto
                {
                    FirstStudentsName = item.FirstStudentsName,
                    SecondStudentsName = item.SecondStudentsName,
                    DocumentAUrl = item.DocumentAUrl,
                    DocumentBUrl = item.DocumentBUrl,
                    SubmittedAt = item.SubmittedAt,
                    PercentageSimilarity = item.PercentageSimilarity,
                    SubmittedBy = item.SubmittedBy,
                    TextExplanation = item.TextExplanation
                };
                comparisonDetailsToReturn.Add(itemsToBeAdded);
            }
            return Ok(comparisonDetailsToReturn);
        }
    }
}
