using BookMyShow.Models;
using System.Collections.Generic;

namespace BookMyShow.Repository.IRepository
{
    public interface IEventRepository
    {
        List<Event> GetAllEvents();
        void AddEvent(Event e);
        void UpdateEvent(Event e);
        void RemoveEvent(Event e);
    }
}
