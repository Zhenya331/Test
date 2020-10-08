using System.Collections.Generic;

namespace Test.Models
{
    public partial class Users
    {
        public Users()
        {
            Images = new HashSet<Images>();
        }

        public long Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }

        public virtual ICollection<Images> Images { get; set; }
    }
}
