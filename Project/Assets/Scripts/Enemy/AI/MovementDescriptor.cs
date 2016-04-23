using UnityEngine;


namespace Assets.Scripts.Enemy.AI
{
    public enum eMovementPathType
    {
        LINEAR = 0,
        SPHERICAL,
    }

    public class MovementDescriptor
    {
        public Vector3 targetPosition;
        public eMovementPathType pathType;

        public MovementDescriptor(Vector3 _targetPosition, eMovementPathType _pathType)
        {
            targetPosition = _targetPosition;
            pathType = _pathType;
        }
    }
}
