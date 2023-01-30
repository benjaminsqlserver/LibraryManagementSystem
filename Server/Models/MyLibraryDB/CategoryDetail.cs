using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Server.Models.MyLibraryDB
{
    [Table("CategoryDetails", Schema = "dbo")]
    public partial class CategoryDetail
    {

        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("@odata.etag")]
        public string ETag
        {
                get;
                set;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string CategoryName { get; set; }

        public ICollection<BookDetail> BookDetails { get; set; }

    }
}