namespace SocialNetwork.Dal.ORM
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Person")]
    public partial class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        [Column(TypeName = "image")]
        public byte[] Avatar { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
