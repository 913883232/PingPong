using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {
    public Text count;
    private Sequence mySequence;
    #region 方法一
    //void Start()
    //{
    //    StartCoroutine(Down());
    //    EventService.Instance.GetEvent<GameStartEvent>().Subscribe(StopBlink);
    //}
    //IEnumerator Down()
    //{
    //    count.text = length.ToString();
    //    mySequence = DOTween.Sequence();
    //    for (int i = length; i > 0; i--)
    //    {
    //        mySequence.Append(count.DOFade(0, 0.5f));
    //        mySequence.Append(count.DOFade(1, 0.5f));
    //        yield return new WaitForSeconds(2f);
    //        count.text = (i - 1).ToString();
    //    }
    //    count.enabled = false;
    //}
    //public void StopBlink()
    //{
    //    mySequence.Kill();
    //    count.DOFade(0, 0.5f);
    //}
    #endregion
    #region 方法二
    private void Awake()
    {
        count = GetComponent<Text>();
        count.enabled = false;
        EventService.Instance.GetEvent<PlayerReGoEvent>().Subscribe(ReGoCount);
        EventService.Instance.GetEvent<PlayerDeadEvent>().Subscribe(DeadCount);
    }
    
    public void StartCount()
    {
        StartCoroutine(DoStartCount());
    }
    public void DeadCount()
    {
        count.enabled = true;
        transform.localScale = Vector3.one;
        count.DOFade(1, 0f);
        count.text = "Ready";
    } 
    public void ReGoCount()
    {
        StartCoroutine(DoReGoCount());
    }
    IEnumerator DoReGoCount()
    {
        count.enabled = true;
        yield return StartCoroutine(Fade("Ready"));
        yield return StartCoroutine(Fade("Go"));
        count.enabled = false;
        GameManager.Instance.PlayerRunEvent();
    }
    IEnumerator DoStartCount()
    {
        count.enabled = true;
        int length = 3;
        for (int i = length; i > 0; i--)
        {
            yield return StartCoroutine(Fade(i.ToString()));
        }
        yield return StartCoroutine(Fade("Go"));
        count.enabled = false;
        GameManager.Instance.PlayerRunEvent();
    }
    IEnumerator Fade(string index)
    {
        transform.localScale = Vector3.one;
        transform.DOScale(1.5f, 1.5f);
        count.text = index.ToString();
        count.DOFade(1, 0f);
        yield return new WaitForSeconds(1f);
        count.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
    }
    #endregion
}
