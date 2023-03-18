using System;
using System.Collections.Generic;

namespace C2_B2.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Movies = new HashSet<Movie>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
