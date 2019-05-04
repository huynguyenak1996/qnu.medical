using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medical.Entities.System
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public string ClaimId { get; set; }
    }
}
