using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExchangeWeb.DAL.Entities
{
    [Table("T_Participant")]
    public class Participant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [InverseProperty("Seller")]
        public virtual ICollection<Trade> Sellers { get; set; }

        [InverseProperty("Customer")]
        public virtual ICollection<Trade> Customers { get; set; }

        public Participant()
        {
            this.Sellers = new List<Trade>();
            this.Customers = new List<Trade>();
        }
    }
}