using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;

#region Types
public enum PlayerState
{
    Idel,
    Run
}
#endregion
[Serializable]
public struct BallInitData
{
    public PlayerState state;
    public float speed;
    public bool isStarted;
}
public class BallMove : MonoBehaviour {
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private BallInitData pingPongInitData;
    private BallInitData currentPingPongData;

    private Transform trans;
    private Transform playerTrans;
    private TrailRenderer trail;
    private Rigidbody2D rig2D;
    private BoxCollider2D coll2D;
    private MainPlayer playerRacket;
    private Vector3 relativePosition;
    private Vector3 currentDirection;
    
    #region Private Fields
    private StateMachine<PlayerState> stateMachine;
    #endregion
    void Awake()
    {
        trans = GetComponent<Transform>();
        coll2D = GetComponent<BoxCollider2D>();
        rig2D = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
    }
    private void Start()
    {
        currentPingPongData = pingPongInitData;
        playerTrans = GameManager.Instance.Player.transform;
        playerRacket = playerTrans.GetComponent<MainPlayer>();
        relativePosition = trans.position - playerTrans.position;
        currentDirection = (playerRacket.CurrentSpeed.normalized + Vector3.up).normalized;

        stateMachine = new StateMachine<PlayerState>();
        stateMachine.AddState(
            PlayerState.Idel,
            () => { trail.enabled = false; });
        stateMachine.AddState(
            PlayerState.Run,
            () => { trail.enabled = true; });
        stateMachine.CurrentState = PlayerState.Idel;
    }
    void Update()
    {
        stateMachine.Update();
        if (stateMachine.CurrentState == PlayerState.Idel && Input.touchCount>0)//.GetKeyDown(KeyCode.Space))
        {
            stateMachine.CurrentState = PlayerState.Run;
        }
        else if (stateMachine.CurrentState == PlayerState.Idel)
        {
            currentDirection = (playerRacket.CurrentSpeed.normalized + Vector3.up).normalized;
            trans.position = relativePosition + playerTrans.transform.position;
            return;
        }
        ReflexLine2();
    }
    private void FixedUpdate()
    {
        if (stateMachine.CurrentState == PlayerState.Idel)
            return;
        rig2D.velocity = currentDirection * currentPingPongData.speed;
    }

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
        stateMachine.CurrentState = PlayerState.Idel;
    }
}
