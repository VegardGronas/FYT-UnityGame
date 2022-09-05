using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOrientation : MonoBehaviour
{
    //REFERENCE TO OTHER SCRIPTS
    private PlayerOrientation[] po;
    private Transform target;
    private PlayerManager pm;
    private PlayerMovement movement;
    private Animator anim;
    //________________________

    //SERIALIZEFIELD PRIVATE
    [SerializeField]
    private float damper;
    [SerializeField]
    private Vector2 distance;
    //________________________

    private void Start()
    {
        pm = GetComponent<PlayerManager>();
        po = FindObjectsOfType<PlayerOrientation>();
        movement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();

        foreach (PlayerOrientation other in po)
        {
            if (other != this)
            {
                target = other.gameObject.transform;
            }
        }
    }

    private void Update()
    {
        if (movement.grounded && !anim.GetBool("Dead"))
        {
            distance = target.position - transform.position;
            distance.y = 0;
            var rotation = Quaternion.LookRotation(distance);

            if (distance.x > 0)
            {
                pm.PlayerRotation = 1;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damper);
            }
            else if (distance.x < 0)
            {
                pm.PlayerRotation = -1;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damper);
            }
        }
    }
}
