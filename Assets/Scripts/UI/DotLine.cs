using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotLine : MonoBehaviour {
    [SerializeField]
    private Sprite sprite1, sprite2;
    private SpriteRenderer _renderer;
    private BoxCollider2D coll;
	void Awake () {
        _renderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
	}
	private void DoSetDotLine(bool isDotLine)
    {
        _renderer.sprite = isDotLine ? sprite1 : sprite2;
        coll.enabled = isDotLine ? false : true;
    }
    public void SetDotLine()
    {
        StartCoroutine(SetDotLineYield());
    }
    public IEnumerator SetDotLineYield()
    {
        DoSetDotLine(false);
        yield return new WaitForSeconds(5f);
        DoSetDotLine(true);
    }
}
