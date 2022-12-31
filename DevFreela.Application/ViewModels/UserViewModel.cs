using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.ViewModels {
    public class UserViewModel 
    {
        public UserViewModel(string userName, string email)
        {
            FullName = userName;
            Email = email;
        }

        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
