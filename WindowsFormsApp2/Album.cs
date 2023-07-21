using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    internal class Album
    {
        public int ID { get; set; }
        public String AlbumName { get; set; }
        public String ArtistName { get; set; }
        public int Year { get; set; }
        public String ImageURL { get; set; }
        public String description { get; set; }
        // make List<Track> songs
    }
}
