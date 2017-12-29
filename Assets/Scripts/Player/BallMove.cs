using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour {
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float speed;
    [SerializeField]
    private LayerMask layerMask;

    private Vector3[] points = new Vector3[4];
    private Vector2[] rayDirection = new Vector2[4];
    
    private Transform trans;
    private Rigidbody2D rig2D;
    private BoxCollider2D coll2D;
    private Vector3 leftPoint, rightPoint, upPoint, downPoint;
    private Vector3 relativePosition;
    private Vector3 currentDirection;
    public Vector3 FixDirection { get; private set; }
    private bool isStart;

    void Awake()
    {
        trans = GetComponent<Transform>();
        coll2D = GetComponent<BoxCollider2D>();
        rig2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        relativePosition = trans.position - player.transform.position;
        currentDirection = new Vector3(Random.Range(-2/3f, 2/3f), Random.value,0).normalized;

        points[0] = new Vector3(-coll2D.bounds.extents.x, 0);
        points[1] = new Vector3(0, coll2D.bounds.extents.y);
        points[2] = new Vector3(coll2D.bounds.extents.x, 0);
        points[3] = new Vector3(0, -coll2D.bounds.extents.y);

        rayDirection[0] = Vector2.left;
        rayDirection[1] = Vector2.up;
        rayDirection[2] = Vector2.right;
        rayDirection[3] = Vector2.down;
    }
    void Update()
    {
        
        if (!isStart && Input.GetKeyDown(KeyCode.Space))
        {
            isStart = true;
        }
        else if (!isStart)
        {
            trans.position = relativePosition + player.transform.position;
            return;
        }
        DetecteRaycasts();
    }
    private void FixedUpdate()
    {
        if (!isStart)
            return;
        rig2D.velocity = speed * currentDirection;
    }
    private void DetecteRaycasts()
    {
        for (int i = 0; i < 4; i++)
        {
            Debug.DrawLine(trans.position + points[3], trans.position + points[3] + Vector3.down * 0.05f, Color.red);
            RaycastHit2D resultPoints = Physics2D.Raycast(trans.position + points[i], rayDirection[i],0.05f,layerMask);
            if (resultPoints.collider != null)
            {
                Vector3 fixDirection = Vector3.zero;
                if (resultPoints.collider.GetComponent<MainPlayer>())
                {
                    fixDirection = resultPoints.collider.GetComponent<MainPlayer>().CurrentSpeed.normalized * 0.5f;
                }
                currentDirection = (Vector3.Reflect(currentDirection, resultPoints.normal) + fixDirection).normalized;
                break;
            }
        }
    }
}
