using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTest : MonoBehaviour {
    public Vector2 startPos;
    public Vector2 direction;
    public bool directionChosen;
    private float directionX;
    [SerializeField]
    private float speed;
    private Transform trans;
    private Vector2 leftPoint, rightPoint;
    private BoxCollider2D coll2D;
    [SerializeField]
    private LayerMask layerMask;
    public Vector2 CurrentSpeed { get; private set; }
    private Vector2 lastPosition;

    private void Awake()
    {
        trans = GetComponent<Transform>();
        coll2D = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        leftPoint = new Vector2(-coll2D.bounds.extents.x, 0);
        rightPoint = new Vector2(coll2D.bounds.extents.x, 0);
        lastPosition = trans.position;
    }
    void Update () {
        RaycastHit2D resultLeft = Physics2D.Raycast(trans.position + (Vector3)leftPoint, Vector2.left, 0.01f, layerMask);
        RaycastHit2D resultRight = Physics2D.Raycast(trans.position + (Vector3)rightPoint, Vector2.right, 0.01f, layerMask);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    trans.position = (Vector2)touch.position/200;
                    directionChosen = false;
                    break;
                    
                case TouchPhase.Moved:
                    direction = touch.position/200 - (Vector2)trans.position;
                    this.trans.transform.position += (Vector3)direction * Time.deltaTime * speed;
                    break;
                    
                case TouchPhase.Ended:
                    directionChosen = true;
                    break;
            }
        }
        if (directionChosen)
        {
            // Something that uses the chosen direction...
        }
        //directionX = Input.GetAxisRaw("Horizontal");
        //if (resultLeft.collider == null && directionX < 0)
        //{
        //    this.trans.transform.position += new Vector3(directionX * Time.deltaTime * speed, 0, 0);
        //}
        //else if (resultRight.collider == null && directionX > 0)
        //{
        //    this.trans.transform.position += new Vector3(directionX * Time.deltaTime * speed, 0, 0);
        //}
        //Debug.DrawLine(trans.position + leftPoint, trans.position + leftPoint + Vector3.left * 0.05f);
        //Debug.DrawLine(trans.position + rightPoint, trans.position + rightPoint + Vector3.right * 0.05f);
        CurrentSpeed = (trans.position - (Vector3)lastPosition) / Time.deltaTime;
        lastPosition = trans.position;
    }
}
