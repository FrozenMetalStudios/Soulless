using System.Collections.Generic;
using Assets.Scripts.Common;


namespace Assets.Scripts
{

    public class BaseManager
    {
        public ManagerContext  managerContext;

        public virtual ARKStatus FindSubmanager(string identity)
        {
            

            // Find all attached Submanagers and add them to our internal map
            return ARKStatus.ARK_NOT_IMPLEMENTED;
        }
    }
}
