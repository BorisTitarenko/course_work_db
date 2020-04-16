using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cource_work.Models.Entity;
using cource_work.ViewModels;

namespace cource_work.Services
{
    public interface ITicketsService {
        IEnumerable<Trip> GetTrips();
    }
    public class TicketsService
    {
    }
}
