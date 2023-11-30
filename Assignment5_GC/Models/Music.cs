using System.ComponentModel.DataAnnotations;

namespace Assignment5_GC.Models
{
    public class Music
    {
        public required int MusicId { get; set; }

        public required string Genre { get; set; }

        public required int SongName { get; set; }

        public required decimal Musicians { get; set; }

    }
}
