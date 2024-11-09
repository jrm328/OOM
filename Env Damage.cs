using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvDamage : MonoBehaviour
{
    [SerializeField]
    private int damageAmount = 1;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private bool isPlayerInRange = false;

    public void DealDamage(int damageAmount)
    {
        if (isPlayerInRange == true)
        {
            var hittable = target.GetComponent<IHittable>();
            hittable?.GetHit(damageAmount, gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
