using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataPannel : MonoBehaviour {
    [SerializeField]
    private Text score, life;
    private GameManager gm;
	void Start () {
        gm = GameManager.Instance;
	}
	
	void Update () {
        life.text = gm.Life.ToString();
        score.text = gm.Score.ToString();
	}
}
