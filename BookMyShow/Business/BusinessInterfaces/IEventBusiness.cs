using BookMyShow.Models;
using System.Collections.Generic;

namespace BookMyShow.Business.BusinessInterfaces
{
    public interface IEventBusiness
    {
        public List<Event> GetAllEvents(string organizerId=null);
        public Event GetEvent(int? id, string userId = null);
        public void CreateEvent(Event e);
        public bool DeleteEvent(int id, string organizerId=null);
        public bool DecrementTicket(int id, int numberOfTickets);
    }
}
