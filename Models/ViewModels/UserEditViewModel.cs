using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectC_v2.Models.ViewModels
{
    public sealed class UserEditViewModel : User
    {
        public new string Email { get; set; }
        public UserEditViewModel(User user)
        {
            this.Birthdate = user.Birthdate;
            this.City = user.City;
            this.CityId = user.CityId;
            this.Email = user.Email;
            this.Name = user.Name;
            this.Password = user.Password;
            this.PostalCode = user.PostalCode;
            this.RoleId = user.RoleId;
            this.Street = user.Street;
            this.Surname = user.Surname;
            this.UserId = user.UserId;
            this.UserRole = user.UserRole;
        }
        public UserEditViewModel()
        {

        }
    }
}
