using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Server.Models.MyLibraryDB
{
    [Table("BorrowerDetails", Schema = "dbo")]
    public partial class BorrowerDetail
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
        public long ID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public long BookID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public DateTime BorrowedFrom { get; set; }

        [Required]
        [ConcurrencyCheck]
        public DateTime BorrowedTo { get; set; }

        [ConcurrencyCheck]
        public DateTime? ActualReturnDate { get; set; }

        [Required]
        [ConcurrencyCheck]
        public long BorrowedBy { get; set; }

        [Required]
        [ConcurrencyCheck]
        public long IssuedBy { get; set; }

        public BookDetail BookDetail { get; set; }

        public LibraryClient LibraryClient { get; set; }

        public LibraryEmployee LibraryEmployee { get; set; }

    }
}