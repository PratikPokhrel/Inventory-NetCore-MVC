using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;


namespace Inventory.DAL.Entities
{
  // public class ApplicationUser : IdentityUser<Guid>
  public class ApplicationUserToken : IdentityUserToken<Guid>
  {
    public ApplicationUserToken() : base()
    {
    }
  }
}
