using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GrislyGrotto
{
    public class Post
    {
        [Key]
        public string Key { get; set; }
        [Required]
        public string Title { get; set; }
        [ForeignKey("Author_Username")]
        public virtual Author Author { get; set; }
        public DateTime Date { get; set; }

        [Required, Column(TypeName="ntext"), MaxLength]
        public string Content { get; set; }
        public int WordCount { get; set; }
        public bool IsStory { get; set; }
        
        public virtual List<Comment> Comments { get; set; }
    }
}