using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Server.Models.MyLibraryDB
{
    [Table("LibraryEmployees", Schema = "dbo")]
    public partial class LibraryEmployee
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
        public long LibraryEmployeeID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string FirstName { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string LastName { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string EmailAddress { get; set; }

        [Required]
        [ConcurrencyCheck]
        public DateTime RegistrationDate { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string Password { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string ConfirmPassword { get; set; }

        public ICollection<BorrowerDetail> BorrowerDetails { get; set; }

    }
}