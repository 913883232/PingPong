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
    private Collider2D coll2D;
    [SerializeField]
    private LayerMask layerMask;
    public Vector3 CurrentSpeed { get; private set; }
    private Vector3 lastPosition;

    private void Awake()
    {
        trans = GetComponent<Transform>();
        coll2D = GetComponent<Collider2D>();
    }
    private void Start()
    {
        leftPoint = new Vector3(-coll2D.bounds.extents.x, 0);
        rightPoint = new Vector3(coll2D.bounds.extents.x, 0);
        lastPosition = trans.position;
    }
    void Update()
    {
        RaycastHit2D resultLeft = Physics2D.Raycast(trans.position + leftPoint, Vector2.left, 0.05f, layerMask);
        RaycastHit2D resultRight = Physics2D.Raycast(trans.position + rightPoint, Vector2.right, 0.05f,layerMask);
        directionX = Input.GetAxis("Horizontal");
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
    }
}
