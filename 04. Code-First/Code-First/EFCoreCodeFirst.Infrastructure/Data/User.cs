using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFCoreCodeFirst.Infrastructure.Data
{
    /// <summary>
    /// Users of the Blog
    /// </summary>
    
    [Table("Users")]
    public class User
    {

        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        [Required]
        [StringLength(50)]
        [Column("FirstName")]
        public string FirstName { get; set; }

        
        [Column("MidleName")]
        public string MidleName { get; set; }


        [Required]
        [StringLength(50)]
        [Column("LastName")]
        public string LastName { get; set; }


        public HashSet<Post> Posts { get; set; } = new HashSet<Post>();

        public HashSet<Comment> Comments { get; set; } = new HashSet<Comment>();

        public HashSet<Replay> Replayes { get; set; } = new HashSet<Replay>();
    }
}
