using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnReward : MonoBehaviour {
    [SerializeField]
    private Vector3 rotateVector3;
    protected Collider2D myColl;
    void Awake () {
        myColl = GetComponent<Collider2D>();
    }
    private void Start()
    {
        rotateVector3 = Random.Range(0f, 1f) > 0.5f ? rotateVector3 : -rotateVector3;
        //rotateVector3 = new Vector3(0, 0, Random.Range(-90, 90));
        transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
    }
    void Update () {
        transform.Rotate(rotateVector3 * Time.deltaTime);
	}
}
