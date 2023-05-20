using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univers.Models.Models
{
    public class AdminUsers
    {
        public string? UserId { get; set; }

        public List<UserModel>? Users { get; set; }

        public UserModel? ChosenUser { get; set; }

        public StudentModel? ChosenStudent { get; set; }

        public EditUserModel? NewUser { get; set; }
    }
}
