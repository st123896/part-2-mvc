using System.ComponentModel.DataAnnotations;

namespace W98.Models
{
    public class Check_login
    {
        [Key] // This attribute specifies that this property is the primary key
        public int Id { get; set; } // Add an Id property as the primary key
        public string email { get; set; }

        public string role { get; set; }

        public string password { get; set; }
    }
}
