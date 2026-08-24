using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP01_SWEB2.Negócio
{
    public class Author
    {
        
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }

        public Author(string name, string email, string gender)
        {
            this.Name = name;
            this.Email = email;
            this.Gender = gender;
        }
    }

}
