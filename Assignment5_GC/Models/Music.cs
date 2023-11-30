using System.ComponentModel.DataAnnotations;

namespace Assignment5_GC.Models
{
    public class Music
    {
<<<<<<< Updated upstream
        public required int MusicId { get; set; }
=======
        public int id { get; set; }
        public string title { get; set; }
        public string purchase_type { get; set; }
        public string performer {  get; set; }
        public string genre { get; set; }
        public int year { get; set; }
        public decimal price { get; set; }
>>>>>>> Stashed changes

        public required string Genre { get; set; }

        public required int SongName { get; set; }

        public required decimal Musicians { get; set; }

    }
}
