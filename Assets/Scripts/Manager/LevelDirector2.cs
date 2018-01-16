using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector2 : LevelDirector {
    [SerializeField]
    private Vector3 playerDownPos, playerUpPos;
    private Vector3 randomPos;
    public override void Decorate()
    {
        downRacket = Instantiate(playerPrefab, playerDownPos, Quaternion.identity);
        upRacket = Instantiate(playerPrefab, playerUpPos, Quaternion.identity);
        if (Random.value > 0.5f)
        {
            randomPos = playerDownPos + new Vector3(0, 0.21f, 0);
            initRacket = downRacket;
        }
        else
        {
            randomPos = playerUpPos - new Vector3(0, 0.21f, 0);
            initRacket = upRacket;
        }
        Instantiate(ballPrefab, randomPos, Quaternion.identity);
    }
}
