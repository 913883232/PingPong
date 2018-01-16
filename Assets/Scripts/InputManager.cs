using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : Singleton<InputManager> {
    [SerializeField]
    private Rect rectUp, rectDown;
    private Touch[] touch;
    public MainPlayer playerUp;
    public MainPlayer playerDown;
    protected override void Awake()
    {
        base.Awake();
        EventService.Instance.GetEvent<PlayerCtrlActiveEvent>().Subscribe(GameStart);
    }
    private void GameStart()
    {
        playerUp = GameManager.Instance.CurrentDirector.UpRacket;
        playerDown = GameManager.Instance.CurrentDirector.DownRacket;
    }

    void Update () {
        foreach (Touch touch in Input.touches)
        {
            if (!GameManager.Instance.GameActived && touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                GameManager.Instance.GameActiveEvent();
            }
            if (GameManager.Instance.GameActived && !GameManager.Instance.PlayerActived && touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                GameManager.Instance.playerReGoEvent();
            }
            else if (playerUp && rectUp.Contains(Camera.main.ScreenToWorldPoint(touch.position)))
            {
                if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                {
                    playerUp.Follow(Camera.main.ScreenToWorldPoint(touch.position));
                }
            }
            else if (playerDown && rectDown.Contains(Camera.main.ScreenToWorldPoint(touch.position)))
            {
                if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                {
                    playerDown.Follow(Camera.main.ScreenToWorldPoint(touch.position));
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawCube(new Vector3(rectDown.x + rectDown.width / 2, rectDown.y + rectDown.height / 2, 0), new Vector3(rectDown.width, rectDown.height, 0));
        Gizmos.DrawCube(new Vector3(rectUp.x + rectUp.width / 2, rectUp.y + rectUp.height / 2, 0), new Vector3(rectUp.width, rectUp.height, 0));
    }
}
