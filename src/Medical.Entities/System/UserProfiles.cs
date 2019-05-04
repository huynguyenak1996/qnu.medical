using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Data;

namespace Medical.Entities.System
{
    #region #================ Class Model UserProfile ====================#
    /// <summary>
    /// This object represents the properties and methods of a UserProfile.
    /// </summary>
    public class UserProfile
    {

        #region Public Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "UserId")]

        public string UserId { get; set; }

        [Display(Name = "FirstName")]

        public string FirstName { get; set; }

        [Display(Name = "LastName")]

        public string LastName { get; set; }

        [Display(Name = "Address")]

        public string Address { get; set; }

        [Display(Name = "Grade")]

        public string Grade { get; set; }

        [Display(Name = "Birthday")]

        public DateTime? Birthday { get; set; }

        [Display(Name = "Avatar")]

        public string Avatar { get; set; }

        [Display(Name = "LastLoginIP")]

        public string LastLoginIP { get; set; }

        [Display(Name = "SignInIP")]

        public string SignInIP { get; set; }

        [Display(Name = "LastLoginDevice")]

        public string LastLoginDevice { get; set; }

        [Display(Name = "Properties")]

        public string Properties { get; set; }

        [Display(Name = "CreatedDate")]

        public DateTime? CreatedDate { get; set; }

        [Display(Name = "CreatedByUser")]

        public string CreatedByUser { get; set; }

        [Display(Name = "ModifiedDate")]

        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "ModifiedByUser")]

        public string ModifiedByUser { get; set; }
        #endregion

    }
    #endregion
}

