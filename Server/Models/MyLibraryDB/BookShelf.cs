using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Server.Models.MyLibraryDB
{
    [Table("BookShelves", Schema = "dbo")]
    public partial class BookShelf
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
        public int ShelfID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int ShelfNo { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int ShelfFloor { get; set; }

        public ICollection<BookDetail> BookDetails { get; set; }

    }
}