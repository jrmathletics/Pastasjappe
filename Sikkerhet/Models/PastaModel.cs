using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Oblig1.Models
{
    public class User
    {
        [Required(ErrorMessage = "Firstname must be added")]
        [StringLength(50, ErrorMessage = "Maximum 50 charactes in firstname")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Surname must be added")]
        [StringLength(50, ErrorMessage = "Maximum 50 characters in surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Address must be added")]
        [StringLength(50, ErrorMessage = "Maximum 50 characters in address")]
        public string Address { get; set; }
        public string Postcode { get; set; }
        [Key]
        [Required(ErrorMessage = "E-mail must be added")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phonenr must be added")]
        [StringLength(20, ErrorMessage = "Maximum 20 characters in phonenr")]
        public string Phonenr { get; set; }
        [Required(ErrorMessage = "Password must be added")]
        [StringLength(25, MinimumLength = 6, ErrorMessage = "Minimum 6 characters in password, maximum 25")]
        public string Password { get; set; }
        [Required]
        public virtual City city { get; set; }
        public virtual List<Order> Orders { get; set; }
    }

    public class dbUser
    {
        [Key]
        public string Email { get; set; }
        public byte[] Password { get; set; }
    }

    public class City
    {
        [Key]
        [Required(ErrorMessage = "Postcode must be added")]
        [StringLength(4, ErrorMessage = "Postcode must be 4 characters")]
        public string Postcode { get; set; }
        [Required(ErrorMessage = "City must be added")]
        [StringLength(50, ErrorMessage = "Maximum 50 characters in city")]
        public string Cityname { get; set; }
    }

    public class Order
    {
        [Key]
        public int Orderid { get; set; }
        public int Orderprice { get; set; }
        public virtual User user { get; set; }
        public virtual List<OrderLine> Orderlines { get; set; }
    }

    public class OrderLine
    {
        [Key]
        public int Orderlineid { get; set; }
        public int Orderid { get; set; }
        public int Itemid { get; set; }
        public int Itemprice { get; set; }
        public int Itemamount { get; set; }
        public virtual Order Order { get; set; }
        public virtual Item Item { get; set; }

    }

    public class Cart
    {
        [Key]
        public int Cartid { get; set; }
        public string Remember { get; set; }
        public int Itemid { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
        public virtual Item Item { get; set; }

    }

    public class Item
    {
        [Key]
        public int Itemid { get; set; }
        public string Itemname { get; set; }
        public int Itemprice { get; set; }
        public int Currentstock { get; set; }
        public string Itemtype { get; set; }
        public string pictureURL { get; set; }
    }

    public class PastaContext : DbContext
    {

        public PastaContext()
            : base("name=PastaModel")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<dbUser> dbUsers { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }

}