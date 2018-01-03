using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    [SerializeField]
    private int life = 3;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private CanvasGroup Win;
    public Transform Player { get { return player; } }
    public int Score { get; set; }

    public int Life
    {
        get
        {
            return life;
        }

        set
        {
            life = value;
            if (life <= 0)
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        print("GamaOver!");
    }
    public void GameWin()
    {
        Win.alpha = 1;
    }
}
