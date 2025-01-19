using autoapprove_dashboard2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace autoapprove_dashboard2.Interfaces
{
    public interface IRequestRepository
    {
        Task<List<Request>> GetRequestsAsync();
        Task<Request> GetRequestByIdAsync(int id);
        Task AddRequestAsync(Request request);
        Task UpdateRequestAsync(Request request);
        Task DeleteRequestAsync(int id);
    }
}
