using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRewards : MonoBehaviour {
    [SerializeField]
    private float repeatRate;
    [SerializeField]
    private SpawnReward[] spawnReward;
	void Start () {
        InvokeRepeating("Creat", 0f, repeatRate);
	}
	
	private void Creat()
    {
        int index = Random.Range(0, spawnReward.Length);
        if (spawnReward.Length > 0)
        Instantiate(spawnReward[index],new Vector2(Random.Range(-2f, 3f), Random.Range(-1f, 6f)), Quaternion.identity);
    }
}
