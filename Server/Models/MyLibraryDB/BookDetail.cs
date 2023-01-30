using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Server.Models.MyLibraryDB
{
    [Table("BookDetails", Schema = "dbo")]
    public partial class BookDetail
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
        public long BookID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string ISBN { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string Language { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int BindingID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int NoOfActualCopies { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int NoOfCurrentCopies { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int CategoryID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int YearOfPublication { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int ShelfID { get; set; }

        public BindingDetail BindingDetail { get; set; }

        public CategoryDetail CategoryDetail { get; set; }

        public BookShelf BookShelf { get; set; }

        public ICollection<BorrowerDetail> BorrowerDetails { get; set; }

    }
}