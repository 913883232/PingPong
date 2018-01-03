using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRewards : MonoBehaviour {
    [SerializeField]
    private float repeatRate;
    [SerializeField]
    private GameObject blockRewardOBJ;
    private GameObject a;
	void Start () {
        InvokeRepeating("Creat", 0f, repeatRate);
	}
	
	private void Creat()
    {
        a = Instantiate(blockRewardOBJ,new Vector2(Random.Range(-2f, 3f), Random.Range(-1f, 6f)), Quaternion.identity);
        Destroy(a, repeatRate);
    }
}
