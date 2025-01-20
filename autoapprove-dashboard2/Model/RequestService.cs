using autoapprove_dashboard2.Data;
using autoapprove_dashboard2.Interfaces;
using autoapprove_dashboard2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace autoapprove_dashboard2.Services
{
    public class RequestService : IRequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceScopeFactory _scopeFactory; 
        private Timer _timer;

        public RequestService(ApplicationDbContext applicationDbContext, IServiceScopeFactory scopeFactory)
        {
            _context = applicationDbContext;
            _scopeFactory = scopeFactory;
            StartAutoApproveRejectTimer();
        }

        private void StartAutoApproveRejectTimer()
        {
            _timer = new Timer(async _ => await AutoApproveRejectRequests(), null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
        }

        private async Task AutoApproveRejectRequests()
        {
            // Ensure DbContext is used within the proper scope
            using (var scope = _scopeFactory.CreateScope())
            {
                var scopedContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var pendingRequests = await scopedContext.Requests.Where(r => r.Status == "Pending").ToListAsync();

                foreach (var request in pendingRequests)
                {
                    if (ShouldApprove(request))
                    {
                        request.Status = "Approved";
                        await UpdateRequestAsync(request, scopedContext);
                    }
                    else
                    {
                        request.Status = "Rejected";
                        await UpdateRequestAsync(request, scopedContext);
                    }
                }
            }
        }

        private bool ShouldApprove(Request request)
        {
            return request.Name.Length % 2 == 0; // Custom logic to auto-approve or reject requests
        }

        public async Task<List<Request>> GetRequestsAsync()
        {
            return await _context.Requests.ToListAsync();
        }

        public async Task<Request> GetRequestByIdAsync(int id)
        {
            return await _context.Requests.FindAsync(id);
        }

        public async Task AddRequestAsync(Request request)
        {
            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRequestAsync(Request request, ApplicationDbContext scopedContext)
        {
            scopedContext.Requests.Update(request);
            await scopedContext.SaveChangesAsync();
        }

        public async Task DeleteRequestAsync(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request != null)
            {
                _context.Requests.Remove(request);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AutoApproveAllRequests()
        {
            var pendingRequests = await _context.Requests.Where(r => r.Status == "Pending").ToListAsync();

            foreach (var request in pendingRequests)
            {
                request.Status = "Approved";
                request.IsChecked = true;
                await UpdateRequestAsync(request, _context);
            }
        }

        public async Task AutoRejectAllRequests()
        {
            var pendingRequests = await _context.Requests.Where(r => r.Status == "Pending").ToListAsync();

            foreach (var request in pendingRequests)
            {
                request.Status = "Rejected";
                await UpdateRequestAsync(request, _context);
            }
        }

        Task IRequestService.UpdateRequestAsync(Request request)
        {
            throw new NotImplementedException();
        }
    }
}

