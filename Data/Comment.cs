using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace GrislyGrotto
{
    public class Comment
    {
        public int Id { get; set; }
        [JsonIgnore]
        public string Post_Key {get;set;}
        [ForeignKey("Post_Key"), JsonIgnore]
        public virtual Post Post { get; set; }

        [Required]
        public string Author { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}