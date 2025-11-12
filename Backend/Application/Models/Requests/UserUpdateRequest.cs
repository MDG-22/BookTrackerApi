using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
   public class UserUpdateRequest
{
    [StringLength(80)]
    public string? Username { get; set; } = string.Empty;

    public string? AvatarUrl { get; set; }

    public string? Description { get; set; }
}

}
