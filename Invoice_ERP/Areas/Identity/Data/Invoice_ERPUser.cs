using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Invoice_ERP.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Invoice_ERPUser class
public class Invoice_ERPUser : IdentityUser
{
    public  string empId { get; set; } = string.Empty;
    public  string firstName { get; set; } = string.Empty;
    public  string lastName { get; set; } = string.Empty;
    public DateTime regDate { get; set; }
}

