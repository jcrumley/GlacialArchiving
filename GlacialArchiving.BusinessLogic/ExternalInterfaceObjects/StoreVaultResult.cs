using System;
using System.Collections.Generic;
using System.Linq;


namespace GlacialArchiving.BusinessLogic.ExternalInterfaceObjects
{
    public class StoreVaultResult
    {
        public bool Valid { get; set; }

        public List<string> Errors { get; set; }

        public ExternalVaultStore ExternalVaultStore { get; set; }
    }
}
