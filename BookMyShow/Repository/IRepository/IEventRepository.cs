using BookMyShow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyShow.Repository.IRepository
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllEvents();
        Task AddEvent(Event e);
        Task UpdateEvent(Event e);
        Task RemoveEvent(Event e);
    }
}
