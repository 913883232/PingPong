using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockReward : SpawnReward {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            myColl.enabled = false;
            GameManager.Instance.currentDirector.DotLine.SetDotLine();
            Destroy(this.gameObject);
        }
    }
}
