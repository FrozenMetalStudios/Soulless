

namespace Assets.Scripts.Enemy.AI
{
    public abstract class IAIBehaviour
    {
        public int priority;

        public abstract bool IsTriggered(BehaviourController controller);
        public abstract void Perform(BehaviourController controller);
    }
}
