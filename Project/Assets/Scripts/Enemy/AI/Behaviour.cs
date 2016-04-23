using UnityEngine;

namespace Assets.Scripts.Enemy.AI
{
    public class Behaviour : MonoBehaviour
    {
        public bool IsTriggered(BehaviourController controller)
        {
            return true;
        }

        public void Perform(BehaviourController controller)
        {
            controller.SetMovementDescriptor(new MovementDescriptor(new Vector3(-6, 0, 0), 0));
        }
    }
}
