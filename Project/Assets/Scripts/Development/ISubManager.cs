using Assets.Scripts.Common;

namespace Assets.Scripts
{
    public delegate void SubmanagerStateChangeCallback(SubmanagerMajorState majorState);

    public class ManagerContext
    {
    }

    public enum SubmanagerMajorState : int
    {
        INIT = 0,
        ACTIVE,
        INACTIVE,
        SHUTDOWN
    }

    public enum SubmanagerStatus : int
    {
        ERROR_NONE = 0,

        ERROR_GENERAL = -500,
        ERROR_STILL_IN_INIT = -501,
    }

    public interface iSubmanager
    {
        string Identify();

        SubmanagerStatus Init(ManagerContext context);

        SubmanagerStatus RegisterStateChangeHandler(SubmanagerStateChangeCallback handler);

        SubmanagerStatus Enable(ManagerContext context);

        SubmanagerStatus Disable(ManagerContext context);

        SubmanagerStatus Shutdown(ManagerContext context);
    }
}
