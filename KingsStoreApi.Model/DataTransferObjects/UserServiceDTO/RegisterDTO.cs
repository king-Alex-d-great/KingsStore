﻿using System;

namespace KingsStoreApi.Model.DataTransferObjects.UserServiceDTO
{
    public class RegisterDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool isAdmin { get; set; }
        public bool isVendor { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string Bio { get; set; }
        public string Password { get; set; }
        public bool isActive { get; set; }
    }
}
