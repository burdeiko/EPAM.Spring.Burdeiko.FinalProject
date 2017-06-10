namespace SocialNetwork.Dal.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public int Id { get; set; }

        [Column("E-Mail")]
        [Required]
        [StringLength(50)]
        public string E_Mail { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public int RoleId { get; set; }

        public virtual Person Person { get; set; }

        public virtual Role Role { get; set; }
    }
}
