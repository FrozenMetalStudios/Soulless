using System.Collections.Generic;
using Assets.Scripts.Common;


namespace Assets.Scripts
{

    public class BaseManager
    {
        public ManagerContext  managerContext;

        public virtual eFmsStatus FindSubmanager(string identity)
        {
            

            // Find all attached Submanagers and add them to our internal map
            return eFmsStatus.FMS_NOT_IMPLEMENTED;
        }
    }
}
