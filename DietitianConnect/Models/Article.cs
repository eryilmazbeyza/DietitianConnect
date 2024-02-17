using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DietitianConnect.Models
{
    public class Article
    {
        [Key]
        public int ArticleID { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int AuthorID { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? Category { get; set; }

    }
}
