using UnityEngine;

namespace Assets.Scripts.Enemy.AI
{
    public class Behaviour : IAIBehaviour
    {
        public Behaviour()
        {
            priority = 0;
        }

        public Behaviour(int _priority)
        {
            priority = _priority;
        }

        public override bool IsTriggered(BehaviourController controller)
        {
            return true;
        }

        public override void Perform(BehaviourController controller)
        {
            controller.SetMovementDescriptor(new MovementDescriptor(new Vector3(-6, 0, 0), 0));
        }
    }
}
