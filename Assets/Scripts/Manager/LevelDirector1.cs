using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector1 : LevelDirector {
    [SerializeField]
    private Vector3 playerDownPos;
    public override void Decorate()
    {
        downRacket = Instantiate(playerPrefab, playerDownPos, Quaternion.identity);
        initRacket = downRacket;
        Instantiate(ballPrefab, playerDownPos + new Vector3(0.22f, 0.22f, 0), Quaternion.identity);
    }
}
