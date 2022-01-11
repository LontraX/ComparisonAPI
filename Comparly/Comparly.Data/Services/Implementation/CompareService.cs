using Comparly.Data.Models;
using Comparly.Data.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparly.Data.Services.Implementation
{
    public class CompareService : ICompareService
    {
        private readonly AppDbContext _ctx;
        public int TotalCount { get; set; }
        public CompareService(AppDbContext appDbContext)
        {
            _ctx = appDbContext;
        }

        private IQueryable<Submission> GetSubmissions()
        {
            var submissions = _ctx.submissions;
            TotalCount = submissions.Count();
            return submissions;
        }
        private async Task<bool> SavedAsync()
        {
            var valueToReturn = false;
            if (await _ctx.SaveChangesAsync() > 0)
                valueToReturn = true;
            else
                valueToReturn = false;

            return valueToReturn;
        }
        public async Task<bool> AddSubmissionAsync(Submission submission)
        {
            await _ctx.submissions.AddAsync(submission);
            return await SavedAsync();
        }

        public async Task<bool> DeleteSubmissionAsync(Submission submission)
        {
            _ctx.submissions.Remove(submission);
            return await SavedAsync();
        }

        public async Task<IEnumerable<Submission>> GetAllSubmissionsAsync(int page, int perPage)
        {
            var query = await GetSubmissions().Skip((page - 1) * perPage).Take(perPage).ToListAsync();
            return query;
        }

        public async Task<Submission> GetSubmissionByIdAsync(string Id)
        {
            var query = await _ctx.submissions.FirstOrDefaultAsync(x => x.Id == Id);
            return query;
        }

        public async Task<bool> UpdateSubmissionAsync(Submission submission)
        {
            _ctx.submissions.Update(submission);
            return await SavedAsync();
        }

        public async Task<IEnumerable<Submission>> GetAllSubmissionsByUser(AppUser appUser)
        {
            var query = await _ctx.submissions.Where(x => x.AppUser.Id == appUser.Id).ToListAsync();
            return query;
        }
    }
}
