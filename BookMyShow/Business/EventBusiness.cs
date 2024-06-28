using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyShow.Business
{
    public class EventBusiness : IEventBusiness
    {
        private readonly IEventRepository eventRepository;
        private readonly IUserRepository userRepository;
        public EventBusiness(IEventRepository eventRepository, IUserRepository userRepository)
        {
            this.eventRepository = eventRepository;
            this.userRepository = userRepository;
        }

        public async Task CreateEvent(Event e)
        {
            await eventRepository.AddEvent(e);       
        }

        public async Task<bool> DecrementTicket(int id,int numberOfTickets)
        {
            var e= await GetEvent(id);
            if (e != null)
            {
                e.NumberOfTickets = e.NumberOfTickets - numberOfTickets;
                await eventRepository.UpdateEvent(e);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteEvent(int id, string organizerId = null)
        {
            var e =await GetEvent(id);
            if (e == null)
            {
                return false;
            }
            if (e.InitialTickets != e.NumberOfTickets)
            {
                return false;
            }
            
            if (organizerId == null)
            {
                await eventRepository.RemoveEvent(e);
                return true;
            }
            if (organizerId != e.UserId)
            {
                return false;
            }
            await eventRepository.RemoveEvent(e);
            return true;

        }

        public async Task<List<Event>> GetAllEvents(string organizerId=null)
        {
            var events = await eventRepository.GetAllEvents();
            if (organizerId == null)
            {
                return events;
            }
            var organizerEvents = events.FindAll(b => b.UserId == organizerId).ToList();
            return organizerEvents;
        }

        public async Task<List<Event>> GetAllEventsByArtistUsername(string artistUsername, string organizerId = null)
        {
            var events = await GetAllEvents();
            var eventsWithArtistUsername = events.Where(e => e.ArtistUsername == artistUsername).ToList();
            if (organizerId == null)
            {
                return eventsWithArtistUsername;
            }
            var eventsWithArtistUsernameOfOrganizers = eventsWithArtistUsername.Where(e => e.UserId == organizerId).ToList();
            return eventsWithArtistUsernameOfOrganizers;
        }

        public async Task<Event> GetEvent(int? id, string userId=null)
        {
            var events =await GetAllEvents();
            var eventChoosen = events.FirstOrDefault(e => e.Id == id);
            if (eventChoosen == null)
            {
                return null;
            }
            if (userId == null)
            {
                return eventChoosen;
            }
            var users=await userRepository.GetAllUsers();
            var user = users.FirstOrDefault(u=> u.IdentityUserId==userId);
            if(user == null || user.IdentityUserId !=eventChoosen.UserId)
            {
                return null;
            }
            return eventChoosen;
        }
    }
}
