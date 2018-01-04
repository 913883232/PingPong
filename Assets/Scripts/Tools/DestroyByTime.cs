using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {
	
	void Start () {
        Invoke("DestroySelf", 5f);
	}
	
	private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
