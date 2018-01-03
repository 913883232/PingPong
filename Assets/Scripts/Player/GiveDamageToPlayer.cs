using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamageToPlayer : MonoBehaviour {
    [SerializeField]
    private int giveDamageToPlayer = 10;
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetComponent<ICanTakeDamage>() != null)
        {
            collision.GetComponent<ICanTakeDamage>().TakeDamage(giveDamageToPlayer, this.gameObject);
        }
    }
}
