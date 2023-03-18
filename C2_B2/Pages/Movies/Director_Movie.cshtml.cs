using C2_B2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace C2_B2.Pages.Movies
{
    public class Director_MovieModel : PageModel
    {
        private readonly PE_PRN_Fall22B1Context context;
        public Director_MovieModel(PE_PRN_Fall22B1Context _context)
        {
            this.context = _context;
        }

        public List<Director> Directors;

        public List<Movie> Movies;
        public int? directorID;


        public void OnGet(int? id, int? deleteId)
        {
            try
            {
                if (id == null)
                {
                    Movies = context.Movies.Include(x => x.Producer).Include(x => x.Stars).ToList();
                }
                else
                {
                    directorID = id;

                    Movies = context.Movies.Include(x => x.Producer).Include(x => x.Stars).Where(x => x.Director.Id == id).ToList();
                }

                Directors = context.Directors.ToList();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
