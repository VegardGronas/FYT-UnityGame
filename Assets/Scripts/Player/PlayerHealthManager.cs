using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    //GETTING REFERENCE TO OTHER SCRIPTS
    private PlayerManager playerManager;
    private Animator anim;
    private PlayerForce playerForce;
    //_________________________________

    //PUBLIC VARIABLES
    public GameObject blockVFX;
    public int AttackRecievedPosition { get; set; }
    public bool IsBlockingLow { get; set; }
    public bool IsBlockingStandard { get; set; }
    public bool IsDead { get; set; }
    //_____________________

    //PRIVATE VARIABLES
    private Transform startPosition;
    //__________________________

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerManager = GetComponent<PlayerManager>();
        playerForce = GetComponent<PlayerForce>();
        startPosition = transform.parent;
    }

    public void BlockAttack()
    {
        playerForce.ResetForce(Vector2.zero);
        if (IsBlockingStandard)
        {
            InstantiateBlockVFX(new Vector3(transform.position.x, 1f, .5f * playerManager.PlayerRotation));
            anim.SetTrigger("BlockFront");
        }
        else
        {
            InstantiateBlockVFX(new Vector3(transform.position.x, .5f, .5f * playerManager.PlayerRotation));
            anim.SetTrigger("BlockLow");
        }
    }

    private void InstantiateBlockVFX(Vector3 pos)
    {
        GameObject vfx = Instantiate(blockVFX, pos, Quaternion.Euler(1f * playerManager.PlayerRotation, 20 * -playerManager.PlayerRotation, 0));
        Destroy(vfx, .15f);
    }

    public void ResetRound()
    {
        anim.SetBool("Dead", false);
        IsDead = false;
        transform.SetPositionAndRotation(startPosition.position, startPosition.rotation);
    }

    public void RecieveHitInfo(float dmg, Vector2 force, ForceMode mode, Vector2 fDruation, Vector2 lTarget, AttackPosition atckPos)
    {
        playerManager.UpdatePlayerHealth(dmg);
        playerForce.AddForce(new Vector2(-force.x, force.y), lTarget, fDruation, mode);
    }
}
