using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AddressBook.Models;

namespace AddressBook.Data
{
    public class ApplicationDbContext : IdentityDbContext<AddressUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AddressBook.Models.Profile> Profile { get; set; }
        public DbSet<AddressBook.Models.PersonalAddress> PersonalAddresses { get; set; }
    }
}
