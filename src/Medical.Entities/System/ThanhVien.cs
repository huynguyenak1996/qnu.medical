using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medical.Entities.System
{
    #region #================ Class Model VNC_ThanhVien ====================#
    /// <summary>
    /// This object represents the properties and methods of a VNC_ThanhVien.
    /// </summary>
    public class VNC_ThanhVien
    {

        #region Public Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "UserID")]

        public string UserID { get; set; }

        [Display(Name = "DoiTuongID")]

        public long DoiTuongID { get; set; }
        #endregion

    }
    #endregion
}

