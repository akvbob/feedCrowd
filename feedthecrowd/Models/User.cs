using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FeedTheCrowd.Models
{
    public partial class User
    {
        public User()
        {
            Comment = new HashSet<Comment>();
            Recipe = new HashSet<Recipe>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Photo { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Recipe> Recipe { get; set; }
    }
}
