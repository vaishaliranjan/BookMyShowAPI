using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyShow.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _dbContext;
        public EventRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddEvent(Event e)
        {
            await _dbContext.Events.AddAsync(e);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Event>> GetAllEvents()
        {
            return await _dbContext.Events.ToListAsync();
        }

        public async Task RemoveEvent(Event eve)
        {
            var eventChoosen = await _dbContext.Events.FirstOrDefaultAsync(e => e.Id == eve.Id);
            _dbContext.Events.Remove(eventChoosen);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateEvent(Event eve)
        {
            var eventChoosen = await _dbContext.Events.FirstOrDefaultAsync(e=> e.Id==eve.Id);
            eventChoosen.NumberOfTickets = eve.NumberOfTickets;
            await _dbContext.SaveChangesAsync();
        }
    }
}
