using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Medical.Entities.System
{
    public class vnc_Modules
    {
        #region Public Properties
        [Key]
        public int Id { get; set; }

        public int ParentID { get; set; }

        public string Name { get; set; }
        //public string Alias { get; set; }
        public string Description { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Class { get; set; }

        public string Icon { get; set; }

        public string Properties { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }

        public int? IsAccess { get; set; }
        #endregion
    }
}
