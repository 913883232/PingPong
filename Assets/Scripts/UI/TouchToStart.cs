using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TouchToStart : MonoBehaviour
{
    public Text count;
    private Sequence mySequence;
    public CountDown countDown;
    #region 方法一
    void Start()
    {
        mySequence = DOTween.Sequence();
        mySequence.Append(transform.GetComponent<Text>().DOColor(Color.red, 1f));
        mySequence.Append(transform.GetComponent<Text>().DOColor(new Color(255, 0, 0, 0), 1f));
        mySequence.SetLoops(-1);
        EventService.Instance.GetEvent<GameActiveEvent>().Subscribe(StopBlink);
    }
    public void StopBlink()
    {
        mySequence.Kill();
        count.DOFade(0, 0.5f);
        countDown.StartCount();
    }
    #endregion
    #region 方法二
    //private void Awake()
    //{
    //    count = GetComponent<Text>();
    //}
    //void Start()
    //{
    //    mySequence = DOTween.Sequence();
    //    mySequence.SetDelay(1);
    //    mySequence.Append(count.DOFade(0, 0.5f));
    //    mySequence.Append(count.DOFade(1, 0.5f));
    //    mySequence.SetLoops(-1);
    //    EventService.Instance.GetEvent<GameStartEvent>().Subscribe(StopBlink);
    //}
    //public void StopBlink()
    //{
    //    mySequence.Kill();
    //    count.DOFade(0, 0.5f);
    //}
    #endregion
}
