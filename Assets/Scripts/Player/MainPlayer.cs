using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    private float directionX;
    [SerializeField]
    private float speed;
    private Transform trans;
    private Vector3 leftPoint, rightPoint;
    private BoxCollider2D coll2D;
    [SerializeField]
    private LayerMask layerMask;
    public Vector3 CurrentSpeed { get; private set; }
    private Vector3 lastPosition;
    [SerializeField]
    private GameObject racket;
    public float minX, maxX;
    public Vector3 currentVelocity;

    private void Awake()
    {
        trans = GetComponent<Transform>();
        coll2D = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        leftPoint = new Vector3(-coll2D.bounds.extents.x, 0);
        rightPoint = new Vector3(coll2D.bounds.extents.x, 0);
        lastPosition = trans.position;
    }
    void Update()
    {
        
    }
    private void Move(float _directionX)
    {
        RaycastHit2D resultLeft = Physics2D.Raycast(trans.position + leftPoint, Vector2.left, 0.01f, layerMask);
        RaycastHit2D resultRight = Physics2D.Raycast(trans.position + rightPoint, Vector2.right, 0.01f, layerMask);
        directionX = _directionX;
        if (resultLeft.collider == null && directionX < 0)
        {
            this.trans.transform.position += new Vector3(directionX * Time.deltaTime * speed, 0, 0);
        }
        else if (resultRight.collider == null && directionX > 0)
        {
            this.trans.transform.position += new Vector3(directionX * Time.deltaTime * speed, 0, 0);
        }
        Debug.DrawLine(trans.position + leftPoint, trans.position + leftPoint + Vector3.left * 0.05f);
        Debug.DrawLine(trans.position + rightPoint, trans.position + rightPoint + Vector3.right * 0.05f);
        CurrentSpeed = (trans.position - lastPosition) / Time.deltaTime;
        lastPosition = trans.position;
        trans.position = new Vector3(Mathf.Clamp(trans.position.x, minX, maxX), trans.position.y, trans.position.z);
    }
    public void Follow(Vector3 tagPos)
    {
        Vector3 temp = Vector3.SmoothDamp(trans.position,tagPos,ref currentVelocity, 0.1f);
        trans.position = new Vector3(temp.x, trans.position.y, trans.position.z);
        CurrentSpeed = (trans.position - lastPosition) / Time.deltaTime;
        lastPosition = trans.position;
        trans.position = new Vector3(Mathf.Clamp(trans.position.x, minX, maxX), trans.position.y, trans.position.z);
    }
}
