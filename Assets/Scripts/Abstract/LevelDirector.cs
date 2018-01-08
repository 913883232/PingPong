using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelDirector : MonoBehaviour {
    [SerializeField]
    private SpawnRewards rewardSpawner;
    [SerializeField]
    private DotLine dotLine;
    public DotLine DotLine { get { return dotLine; } }
    [SerializeField]
    protected MainPlayer playerPrefab;
	void Start () {
		
	}

    public abstract void Decorate();
}
