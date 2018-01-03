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
    [SerializeField]
    private CanvasGroup Over;
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
        Over.alpha = 1;
        Time.timeScale = 0;
    }
    public void GameWin()
    {
        Win.alpha = 1;
        Time.timeScale = 0;
    }
}
