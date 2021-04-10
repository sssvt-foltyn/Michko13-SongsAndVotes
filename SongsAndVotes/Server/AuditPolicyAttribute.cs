using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SongsAndVotes.Server
{



    public class AuditPolicyAttribute
    {



        public bool NeedsAudit { get; }



        public AuditPolicyAttribute(bool needsAudit)
        {
            NeedsAudit = needsAudit;
        }



    }



}
