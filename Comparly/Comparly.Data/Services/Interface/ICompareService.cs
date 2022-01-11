using Comparly.Data.Dtos;
using Comparly.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparly.Data.Services.Interface
{
    public interface ICompareService
    {
        Task<Submission> GetSubmissionByIdAsync(string Id);
        Task<IEnumerable<Submission>> GetAllSubmissionsAsync(int page, int perPage);

        Task<bool> UpdateSubmissionAsync(Submission submission);
        Task<IEnumerable<Submission>> GetAllSubmissionsByUser(AppUser appUser);
        Task<bool> DeleteSubmissionAsync(Submission submission);
        Task<bool> AddSubmissionAsync(Submission submission);
    }
}
