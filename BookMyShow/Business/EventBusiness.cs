using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Data;
using BookMyShow.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookMyShow.Business
{
    public class EventBusiness : IEventBusiness
    {
        private AppDbContext _appDbContext;
        public EventBusiness(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
 

        public void CreateEvent(Event e)
        {     
                _appDbContext.Events.Add(e);
                _appDbContext.SaveChanges();          
        }

        public void DecrementTicket(int id,int numberOfTickets)
        {
            var e= _appDbContext.Events.FirstOrDefault(e => e.Id == id);
            e.NumberOfTickets -= numberOfTickets;
            _appDbContext.SaveChanges();
        }

        public bool DeleteEvent(int id, string organizerId = null)
        {
            var e= _appDbContext.Events.FirstOrDefault(e=>e.Id==id);
            //check if same organizer
            if (e.InitialTickets != e.NumberOfTickets)
            {
                return false;
            }
            if (e == null)
            {
                return false;
            }
            if (organizerId == null)
            {
                _appDbContext.Events.Remove(e);
                _appDbContext.SaveChanges();
                return true;
            }
            if (organizerId != e.UserId)
            {
                return false;
            }
            _appDbContext.Events.Remove(e);
            _appDbContext.SaveChanges();
            return true;

        }

        public List<Event> GetAllEvents(string organizerId=null)
        {
            var events = _appDbContext.Events.ToList();
            if (organizerId == null)
            {
                return events;
            }
            var organizerEvents = events.FindAll(b => b.UserId == organizerId).ToList();
            return organizerEvents;
        }


        public Event GetEvent(int? id, string userId=null)
        {
            var eventChoosen = _appDbContext.Events.Find(id);
            if (eventChoosen == null)
            {
                return null;
            }
            if(userId == null)
            {
                return eventChoosen;
            }
            var user = _appDbContext.Users.Find(userId);
            if(user == null || user.IdentityUserId !=userId)
            {
                return null;
            }
            return eventChoosen;
        }
    }
}
