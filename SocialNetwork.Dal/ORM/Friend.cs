namespace SocialNetwork.Dal.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Friend")]
    public partial class Friend
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonRequestedId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonConfirmedId { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool Confirmed { get; set; }

        public virtual Person Person { get; set; }

        public virtual Person Person1 { get; set; }
    }
}
