﻿using Newtonsoft.Json;

namespace CarSale.Models
{
    public class UserDto
    {
        [JsonIgnore] public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
