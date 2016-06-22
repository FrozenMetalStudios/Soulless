using UnityEngine;
using System.Collections;


abstract public class Health : MonoBehaviour
{
    abstract public void TakeDamage(int amount);
    abstract public void Death();
}


