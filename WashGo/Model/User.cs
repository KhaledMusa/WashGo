﻿namespace WashGo.Model
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; } 
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}