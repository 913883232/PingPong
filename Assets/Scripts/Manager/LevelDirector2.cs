using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector2 : LevelDirector {

    [SerializeField]
    private Vector3 playerDownPos, playerUpPos;
    [SerializeField]
    private BallMove ball;
    private Vector3 randomPos;
    
    public override void Decorate()
    {
        InputManager inputManager = InputManager.Instance;
        GameManager.Instance.Player = Instantiate(playerPrefab, playerUpPos, Quaternion.identity);
        inputManager.playerUp = GameManager.Instance.Player;
        inputManager.playerDown = Instantiate(playerPrefab, playerDownPos, Quaternion.identity);
        randomPos = Random.Range(0f, 1f) > 0.5f ? ball.transform.position : playerUpPos -= new Vector3(0,0.21f,0);
        GameManager.Instance.Ball = Instantiate(ball, randomPos, Quaternion.identity);
    }
}
