using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace ProjectC_v2.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(30)]
        [StringLength(30 , ErrorMessage = "Name cannot be longer than 30 characters")]
        public string Name { get; set; }

        [MaxLength(30)]
        [StringLength(30, ErrorMessage = "Surname cannot be longer than 30 characters")]
        [Required]
        public string Surname { get; set; }

        [Required]
        [MaxLength(200)]
        public string Street { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(6)]
        [StringLength(6,MinimumLength = 6 , ErrorMessage = "A postal code contains 6 characters")]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        [MaxLength(200)]
        [DataType(DataType.Password)]
        [StringLength(200, ErrorMessage = "Password cannot be longer than 200 characters")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at least 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime Birthdate { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(200)]
        [Remote("CheckExistingEmail", "User", HttpMethod = "POST", ErrorMessage = "Email already exists")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "City")]
        public int CityId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Role")]
        public int RoleId { get; set; }
        public virtual UserRole UserRole { get; set; }
        public virtual City City { get; set; }
        //added by robin
        //public virtual ShoppingCart ShoppingCart { get; set; }
    }
}