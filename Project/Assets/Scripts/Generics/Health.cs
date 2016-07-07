using UnityEngine;
using System.Collections;


abstract public class Health : MonoBehaviour
{
    abstract public void TakeDamage(float amount);
    abstract public void Death();
}


