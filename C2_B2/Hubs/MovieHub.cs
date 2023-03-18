using C2_B2.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace C2_B2.Hubs
{
    public class MovieHub : Hub
    {
        private readonly PE_PRN_Fall22B1Context _context;
        public MovieHub(PE_PRN_Fall22B1Context context)
        {
            _context = context;
        }
        public async Task DeleteMovie(int? deleteId)
        {
            if (deleteId != null)
            {
                var movie = _context.Movies
                     .Include(x => x.Stars)
                     .Include(x => x.Genres)
                     .FirstOrDefault(x => x.Id == deleteId);


                foreach (var item in movie.Stars.ToList())
                {
                    movie.Stars.Remove(item);
                }

                foreach (var item in movie.Genres.ToList())
                {
                    movie.Genres.Remove(item);
                }

                foreach (var item in _context.Stars.Include(x => x.Movies).ToList())
                {
                    foreach (var i in item.Movies.ToList())
                    {
                        if (i.Id == deleteId)
                        {
                            item.Movies.Remove(i);
                        }
                    }
                }

                foreach (var item in _context.Genres.Include(x => x.Movies).ToList())
                {
                    foreach (var i in item.Movies.ToList())
                    {
                        if (i.Id == deleteId)
                        {
                            item.Movies.Remove(i);
                        }
                    }
                }
                _context.Movies.Remove(movie);
                _context.SaveChanges();
            }
            await Clients.All.SendAsync("LoadMovie");
        }
    }
}
