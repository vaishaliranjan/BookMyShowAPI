using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace BookMyShow.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _dbContext;
        public EventRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddEvent(Event e)
        {
            _dbContext.Events.Add(e);
            _dbContext.SaveChanges();
        }

        public List<Event> GetAllEvents()
        {
            return _dbContext.Events.ToList();
        }

        public void RemoveEvent(Event eve)
        {
            var eventChoosen = _dbContext.Events.FirstOrDefault(e => e.Id == eve.Id);
                _dbContext.Events.Remove(eventChoosen);
                _dbContext.SaveChanges();
        }

        public void UpdateEvent(Event eve)
        {
            var eventChoosen = _dbContext.Events.FirstOrDefault(e=> e.Id==eve.Id);

                eventChoosen.NumberOfTickets = eve.NumberOfTickets;
                _dbContext.SaveChanges();
        }
    }
}
