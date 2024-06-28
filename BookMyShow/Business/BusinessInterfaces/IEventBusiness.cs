using BookMyShow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyShow.Business.BusinessInterfaces
{
    public interface IEventBusiness
    {
        public Task<List<Event>> GetAllEvents(string organizerId=null);
        public Task<Event> GetEvent(int? id, string userId = null);
        public Task CreateEvent(Event e);
        public Task<bool> DeleteEvent(int id, string organizerId=null);
        public Task<bool> DecrementTicket(int id, int numberOfTickets);

        public Task<List<Event>> GetAllEventsByArtistUsername(string artistUsername,string organizerId=null);
    }
}
