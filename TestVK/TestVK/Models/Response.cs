using System.Collections.Generic;

namespace TestVK.Models
{
    internal class Response
    {
        public int count { get; set; }
        public List<LikedUsers> users { get; set; }
    }
}
