using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager> {
    [SerializeField]
    private Rect rectUp, rectDown;
    private Touch[] touch;
    public MainPlayer playerUp;
    public MainPlayer playerDown;
	void Start () {
		
	}
	
	void Update () {
        foreach (Touch touch in Input.touches)
        {
            if (playerUp && rectUp.Contains(Camera.main.ScreenToWorldPoint(touch.position)))
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
