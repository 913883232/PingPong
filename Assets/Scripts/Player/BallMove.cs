using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;

[Serializable]
public struct BallInitData
{
    public Vector3 initPosition;
    public float speed;
    public bool isStarted;
}
public class BallMove : MonoBehaviour {
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private BallInitData pingPongInitData;
    private BallInitData currentPingPongData;

    //private Vector3[] points = new Vector3[4];
    //private Vector2[] rayDirection = new Vector2[4];

    private Transform trans;
    private Transform playerTrans;
    private Rigidbody2D rig2D;
    private BoxCollider2D coll2D;
    private MainPlayer playerRacket;
    private Vector3 relativePosition;
    private Vector3 currentDirection;
    //private Vector3 leftPoint, rightPoint, upPoint, downPoint;
    //public Vector3 FixDirection { get; private set; }

    void Awake()
    {
        trans = GetComponent<Transform>();
        coll2D = GetComponent<BoxCollider2D>();
        rig2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        currentPingPongData = pingPongInitData;
        playerTrans = GameManager.Instance.Player;
        playerRacket = playerTrans.GetComponent<MainPlayer>();
        relativePosition = trans.position - playerTrans.transform.position;
        //currentDirection = new Vector3(Random.Range(-2/3,2/3), Random.value,0).normalized;

        //points[0] = new Vector3(-coll2D.bounds.extents.x, 0);
        //points[1] = new Vector3(0, coll2D.bounds.extents.y);
        //points[2] = new Vector3(coll2D.bounds.extents.x, 0);
        //points[3] = new Vector3(0, -coll2D.bounds.extents.y);

        //rayDirection[0] = Vector2.left;
        //rayDirection[1] = Vector2.up;
        //rayDirection[2] = Vector2.right;
        //rayDirection[3] = Vector2.down;
    }
    void Update()
    {
        if (!currentPingPongData.isStarted && Input.GetKeyDown(KeyCode.Space))
        {
            currentPingPongData.isStarted = true;
        }
        else if (!currentPingPongData.isStarted)
        {
            currentDirection = (playerRacket.CurrentSpeed.normalized + Vector3.up).normalized;
            trans.position = relativePosition + playerTrans.transform.position;
            return;
        }
        ReflexLine2();
    }
    private void FixedUpdate()
    {
        if (!currentPingPongData.isStarted)
            return;
        rig2D.velocity = currentDirection * currentPingPongData.speed;
    }
    //private void DetecteRaycasts()
    //{
    //    for (int i = 0; i < 4; i++)
    //    {
    //        Debug.DrawLine(trans.position + points[3], trans.position + points[3] + Vector3.down * 0.1f, Color.red);
    //        RaycastHit2D resultPoints = Physics2D.Raycast(trans.position + points[i], rayDirection[i],0.1f,layerMask);
    //        if (resultPoints.collider != null)
    //        {
    //            Vector3 fixDirection = Vector3.zero;
    //            if (resultPoints.collider.GetComponent<MainPlayer>())
    //            {
    //                fixDirection = resultPoints.collider.GetComponent<MainPlayer>().CurrentSpeed.normalized * 0.5f;
    //            }
    //            currentDirection = (Vector3.Reflect(currentDirection, resultPoints.normal) + fixDirection).normalized;
    //            break;
    //        }
    //    }
    //}
    private void ReflexLine2()
    {
        RaycastHit2D results = Physics2D.BoxCast(trans.position, coll2D.bounds.extents * 2.5f, 0f, Vector2.zero, 0f, layerMask);
        if (results.collider != null)
        {
            Vector3 fixDirection = Vector3.zero;
            if (results.collider.GetComponent<MainPlayer>())
            {
                fixDirection = results.collider.GetComponent<MainPlayer>().CurrentSpeed.normalized * 0.5f;
            }
            currentDirection = (Vector3.Reflect(currentDirection, results.normal) + fixDirection).normalized;
            ICanTakeDamage CanTakeDamage = results.collider.GetComponent<ICanTakeDamage>();
            if (CanTakeDamage != null)
            {
                CanTakeDamage.TakeDamage(1, this.gameObject);
            }
        }
    }
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.black;
    //    Gizmos.DrawCube(trans.position, coll2D.bounds.extents * 2.5f);
    //}
    public void DestroySelf()
    {
        GameManager.Instance.Life -= 1;
        ResetPingPongData();
    }
    public void ResetPingPongData()
    {
        currentPingPongData = pingPongInitData;
        trans.rotation = Quaternion.identity;
        rig2D.velocity = Vector2.zero;
        rig2D.angularVelocity = 0f;
    } 
}
