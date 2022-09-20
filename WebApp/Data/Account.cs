using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Data
{
    public class Account : IdentityUser
    {
        public string FirstName { get; set; }  
        public string LastName { get; set; }  
        public string ShippingAddress { get; set; }


    }
}
