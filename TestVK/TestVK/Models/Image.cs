using System.Collections.Generic;

namespace TestVK.Models
{
    internal class Image
    {
        public int Album_id { get; set; }
        public int Date { get; set; }
        public int Id { get; set; }
        public int Owner_id { get; set; }
        public string Access_key { get; set; }
        public List<Size> Sizes { get; set; }
        public string Text { get; set; }
        public bool Has_tags { get; set; }
    }
}
