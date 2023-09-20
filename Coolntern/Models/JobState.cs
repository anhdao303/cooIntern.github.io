using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coolntern.Models
{
    public enum JobState : short
    {
        Approve = 0, 
        WaitApprove = 1,
        Deny = 2,
    }
}