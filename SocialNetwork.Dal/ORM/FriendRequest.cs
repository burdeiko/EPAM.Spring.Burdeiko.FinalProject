namespace SocialNetwork.Dal.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FriendRequest")]
    public partial class FriendRequest
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SenderId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReceiverId { get; set; }

        public bool IsConfirmed { get; set; }

        public virtual Person Receiver { get; set; }

        public virtual Person Sender { get; set; }
    }
}
