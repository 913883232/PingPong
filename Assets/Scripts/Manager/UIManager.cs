using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {
    [SerializeField]
    private GameObject topWall;
	void Start () {
        
	}
	
	void Update () {
		
	}
    public void Creat()
    {
        Instantiate(topWall);
    }
}
