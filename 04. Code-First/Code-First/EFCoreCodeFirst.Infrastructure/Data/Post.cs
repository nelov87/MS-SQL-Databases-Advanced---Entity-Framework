using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFCoreCodeFirst.Infrastructure.Data
{
    /// <summary>
    /// Class for Posts
    /// </summary>

    [Table("Posts")]
    public class Post
    {
        /// <summary>
        /// Id for posts
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// AuthorId of the posts
        /// </summary>
        [Required]
        public int AuthorId { get; set; }

        /// <summary>
        /// Title oh post
        /// </summary>
        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string Title { get; set; }

        /// <summary>
        /// Content of the Post
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// Author of the posts
        /// </summary>
        [ForeignKey(nameof(AuthorId))]
        public User Author { get; set; }

        

    }
}
