using UnityEngine;

namespace Assets.Scripts.Enemy.AI
{
    interface IAIController
    {
        void DetermineNewPosition(EnemyController enemy);
    }
}
