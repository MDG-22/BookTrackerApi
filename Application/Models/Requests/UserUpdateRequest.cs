using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class UserUpdateRequest
    {
        public string Username { get; set; }
        public string AvatarUrl { get; set; }
        public string Description { get; set; }
    }
}
