using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Data;
using BookMyShow.Models;
using BookMyShow.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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


        public void CreateEvent(Event e)
        {
            eventRepository.AddEvent(e);       
        }

        public bool DecrementTicket(int id,int numberOfTickets)
        {
            var e= GetEvent(id);
            if (e != null)
            {
                return false;
            }
            eventRepository.UpdateEvent(e);
            return true;
        }

        public bool DeleteEvent(int id, string organizerId = null)
        {
            var e = GetEvent(id);
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
                eventRepository.RemoveEvent(e);
                return true;
            }
            if (organizerId != e.UserId)
            {
                return false;
            }
            eventRepository.RemoveEvent(e);
            return true;

        }

        public List<Event> GetAllEvents(string organizerId=null)
        {
            var events = eventRepository.GetAllEvents();
            if (organizerId == null)
            {
                return events;
            }
            var organizerEvents = events.FindAll(b => b.UserId == organizerId).ToList();
            return organizerEvents;
        }


        public Event GetEvent(int? id, string userId=null)
        {
            var events = GetAllEvents(userId);
            var eventChoosen = events.FirstOrDefault(e => e.Id == id);
            if (eventChoosen == null)
            {
                return null;
            }
            if(userId == null)
            {
                return eventChoosen;
            }
            var users= userRepository.GetAllUsers();
            var user = users.FirstOrDefault(u=> u.IdentityUserId==userId);
            if(user == null || user.IdentityUserId !=userId)
            {
                return null;
            }
            return eventChoosen;
        }

    }
}
