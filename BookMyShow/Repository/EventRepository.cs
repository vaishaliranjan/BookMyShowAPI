using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using System;
using System.Collections;
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

        public void RemoveEvent(Event e)
        {
            _dbContext.Events.Remove(e);
            _dbContext.SaveChanges();
        }

        public void UpdateEvent(Event e)
        {
            var eventChoosen = _dbContext.Events.Find(e.Id);
            eventChoosen.NumberOfTickets = e.NumberOfTickets;
            _dbContext.SaveChanges();
        }
    }
}
