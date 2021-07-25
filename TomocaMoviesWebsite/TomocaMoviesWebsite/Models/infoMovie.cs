using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TomocaMoviesWebsite.Models
{
    public class infoMovie
    {
        public Movy MovieInf { get; set; }
        public MoviesGenre movieID { get; set; }
        public Genre Category { get; set; }
        public Director director { get; set; }
        public Actor actors { get; set; }
        public YoutubeReview youtubeReview { get; set; }
    }
}