

namespace Assets.Scripts.Enemy.AI
{
    interface IAIBehaviour
    {
        bool IsTriggered(BehaviourController controller);
        void Perform(BehaviourController controller);
    }
}
