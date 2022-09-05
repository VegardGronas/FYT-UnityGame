using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForce : MonoBehaviour
{
    private Rigidbody rigid;
    private PlayerManager pm;

    //_____________
    public Vector2 mForce;
    public Vector2 MForce { get; set; }
    private Vector2 lerpSpeed;
    public Vector2 LTarget { get; set; }
    public ForceMode ForceM { get; set; }

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerManager>();
    }

    private void FixedUpdate()
    {
        mForce.y = Mathf.Lerp(mForce.y, LTarget.y, lerpSpeed.y += Time.fixedDeltaTime);
        if (lerpSpeed.x > 0)
        {
            mForce.x = Mathf.Lerp(mForce.x, LTarget.x, lerpSpeed.x += Time.fixedDeltaTime);
        }
        rigid.AddForce(new Vector3(mForce.x * pm.PlayerRotation, mForce.y, 0), ForceM);
    }

    public void AddForce(Vector2 moveForce, Vector2 lerpTarget, Vector2 lSpeed, ForceMode mode)
    {
        lerpSpeed = Vector2.zero;
        mForce = moveForce;
        LTarget = lerpTarget;
        lerpSpeed = lSpeed;
        ForceM = mode;
    }
    
    public void AddNormalForce(Vector2 moveForce, ForceMode mode)
    {
        lerpSpeed.x = 0;
        mForce = moveForce;
        ForceM = mode;
    }

    public void ResetForce(Vector2 forceStop)
    {
        mForce = forceStop;
    }

    public void AddToExisting(Vector2 force)
    {
        mForce.x = force.x;
    }
}
