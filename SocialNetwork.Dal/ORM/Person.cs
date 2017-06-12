namespace SocialNetwork.Dal.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Person")]
    public partial class Person
    {
        
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Hobbies { get; set; }

        [StringLength(50)]
        public string FavoriteBooks { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        public byte[] Avatar { get; set; }

        public virtual User User { get; set; }
    }
}
