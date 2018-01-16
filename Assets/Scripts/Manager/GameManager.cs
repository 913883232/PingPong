using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private LevelDirector currentDirector;
    public LevelDirector CurrentDirector { get { return currentDirector; } }
    
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
    private bool gameActived;
    public bool GameActived
    {
        get { return gameActived; }
        set { gameActived = value; }
    }
    private bool playerActived;
    public bool PlayerActived
    {
        get { return playerActived; }
        set { playerActived = value; }
    }
    private UIManager uiManager;
    public UIManager UiManager { get; set; }
    private void Start()
    {
        uiManager = UIManager.Instance;
        //EventService.Instance.GetEvent<GameActiveEvent>();
    }
    public void ActiveGame()
    {
        gameActived = true;
        PlayerActived = true;
        StartGame();
    }
    public void StartGame()
    {
        if (currentPlayerCount == PlayerCount.one)
        {
            currentDirector = levelDirector1;
            uiManager.Creat();
        }
            
        else
            currentDirector = levelDirector2;

        currentDirector.Decorate();
        PlayerCtrlActiveEvent();
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
    public void GameActiveEvent()
    {
        EventService.Instance.GetEvent<GameActiveEvent>().Publish();
        ActiveGame();
    }
    public void GameStartEvent()
    {
        EventService.Instance.GetEvent<GameStartEvent>().Publish();
    }
    public void PlayerRunEvent()
    {
        EventService.Instance.GetEvent<PlayerRunEvent>().Publish();
    }
    public void PlayerCtrlActiveEvent()
    {
        EventService.Instance.GetEvent<PlayerCtrlActiveEvent>().Publish();
    }
    public void PlayerDeadEvent()
    {
        PlayerActived = false;
        EventService.Instance.GetEvent<PlayerDeadEvent>().Publish();
    }
    public void playerReGoEvent()
    {
        PlayerActived = true;
        EventService.Instance.GetEvent<PlayerReGoEvent>().Publish();
    }
}
