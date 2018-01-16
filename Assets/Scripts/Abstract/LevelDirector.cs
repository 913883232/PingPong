using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelDirector : MonoBehaviour {
    protected MainPlayer downRacket;
    protected MainPlayer upRacket;
    public MainPlayer DownRacket { get { return downRacket; } }
    public MainPlayer UpRacket { get { return upRacket; } }

    protected MainPlayer initRacket;
    public MainPlayer InitRacket { get { return initRacket; } }

    [SerializeField]
    private SpawnRewards rewardSpawner;
    [SerializeField]
    private DotLine dotLine;
    public DotLine DotLine { get { return dotLine; } }
    [SerializeField]
    protected MainPlayer playerPrefab;
    [SerializeField]
    protected BallMove ballPrefab;
	void Start () {
		
	}

    public abstract void Decorate();
}
