using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFCoreCodeFirst.Infrastructure.Data
{
    [Table("Replayes")]
    public class Replay
    {
        [Key]
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public int CommentId { get; set; }

        [Required]
        [StringLength(250)]
        public string Content { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public User Author { get; set; }

        [ForeignKey(nameof(CommentId))]
        public Comment Comment { get; set; }
    }
}
