using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector1 : LevelDirector {
    [SerializeField]
    private Vector3 playerDownPos;
    [SerializeField]
    private BallMove ball;
    [SerializeField]
    private GameObject topWall;
    public override void Decorate()
    {
        Instantiate(topWall);
        InputManager inputManager = InputManager.Instance;
        GameManager.Instance.Player = Instantiate(playerPrefab, playerDownPos, Quaternion.identity);
        GameManager.Instance.Ball = Instantiate(ball, ball.transform.position, Quaternion.identity);
        inputManager.playerDown = GameManager.Instance.Player;
    }
}
