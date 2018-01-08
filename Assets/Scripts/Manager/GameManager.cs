using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    public enum PlayerCount { one,two }
    [SerializeField]
    private PlayerCount currentPlayerCount;
    [SerializeField]
    private int life = 3;
    [SerializeField]
    private CanvasGroup Win;
    [SerializeField]
    private CanvasGroup Over;
    [SerializeField]
    private LevelDirector levelDirector1;
    [SerializeField]
    private LevelDirector levelDirector2;
    public LevelDirector currentDirector;
    private BallMove ball;
    public BallMove Ball { get; set; }
    private MainPlayer player;
    public MainPlayer Player { get; set; }
    public int Score { get; set; }

    public int Life
    {
        get { return life; }
        set
        {
            life = value;
            if (life <= 0)
                GameOver();
        }
    }
    private UIManager uiManager;
    public UIManager UiManager { get; set; }
    private void Start()
    {
        uiManager = UIManager.Instance;
        StartGame();
    }
    public void StartGame()
    {
        if (currentPlayerCount == PlayerCount.one)
            currentDirector = levelDirector1;
        else
            currentDirector = levelDirector2;
        currentDirector.Decorate();
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
