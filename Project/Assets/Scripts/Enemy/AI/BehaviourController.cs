using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Enemy.AI
{
    public class BehaviourController : MonoBehaviour, IAIController
    {
        public List<Behaviour> behaviours;
        public MovementDescriptor moveDescriptor;
        public MovementDescriptor processedDescriptor;
        
        private float interpolationTime = 0;
        public float transitionSpeed = 18f;
        private Vector3 startingPosition;
        private float lastInterpol;
        public AnimationCurve curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));


        public void DetermineNewPosition(EnemyController enemy)
        {
            foreach (Behaviour iter in behaviours)
            {
                if(iter.IsTriggered(this))
                {
                    iter.Perform(this);
                }
            }

            if (this.moveDescriptor != this.processedDescriptor)
            {
                // Reset movement parameters
                interpolationTime = 0;
                startingPosition = enemy.GetPosition();

                // Set Current Processed Descriptor
                this.processedDescriptor = this.moveDescriptor;
            }

            if (enemy.GetPosition() != processedDescriptor.targetPosition)
            {
                // Update our interpolation time
                interpolationTime += Time.deltaTime;

                // Use the Animation Curve to determine the progress of the Camera
                float totalDistance = Vector3.Distance(startingPosition,
                                                       processedDescriptor.targetPosition);

                // Ensure no division by zero, fixes jump on boundary when cameras are very similar.
                if (totalDistance == 0)
                {
                    enemy.SetPosition(processedDescriptor.targetPosition);
                }
                else
                {
                    // Determine Progress
                    lastInterpol = curve.Evaluate(interpolationTime * transitionSpeed / totalDistance);

                    // Process the movement descriptor
                    Vector3 newPosition = new Vector3();

                    // Update the position from the last cameras position
                    newPosition = Vector3.Lerp(startingPosition,
                                               processedDescriptor.targetPosition,
                                               lastInterpol);
                    
                    newPosition.y = enemy.transform.position.y;
                    newPosition.z = enemy.transform.position.z;

                    enemy.SetPosition(newPosition);
                }
            }
        }

        public void SetMovementDescriptor(MovementDescriptor newDescriptor)
        {
            moveDescriptor = newDescriptor;
        }
    }
}
