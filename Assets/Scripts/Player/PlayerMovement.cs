using UnityEngine;
using UnityEngine.VFX;

public class PlayerMovement : MonoBehaviour
{
    //GETTING SCRIPT REFERENCES
    private PlayerHealthManager playerHealth;
    private PlayerHitManager otherPlayer;
    private Animator anim;
    private PlayerManager playerManager;
    private InputGame inputGame;
    private PlayerForce playerForce;
    private Maps currentMap;
    //___________________________________

    //SERIALIZEFIELD VARIABLES
    [SerializeField]
    private float distanceCollition;
    [SerializeField]
    private float defaultMoveSpeed;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float SprintSpeed;
    [SerializeField]
    private float damper;
    [SerializeField]
    private float distanceToGround;
    [SerializeField]
    private float jumpCD;
    private float setJumpCooldown;
    [SerializeField]
    private Vector2 jumpUp;
    [SerializeField]
    private Vector2 lerpJump;
    [SerializeField]
    private Vector2 gravity;
    [SerializeField]
    private Vector3 currentForce;
    //________________________________

    //PRIVATE VARIABLES_____________
    //_____________________________
    public int Downed { get; set; }
    public GameObject hitGroundVFX;
    public GameObject jumpHitGroundVFX;


    //GRADIENT ON VFX
    public Gradient darkerHitEffectGradient;
    public Gradient lightHitEffectGradient;
    //___________________________

    //BOOLEANS___________
    public bool isHit;
    public bool isBlocking;
    public bool grounded;
    private bool jumping;
    //__________________________________________________________

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        inputGame = GetComponentInParent<InputGame>();
        inputGame.PlayerMovement = this;
        otherPlayer = GetComponent<PlayerHitManager>();

        playerHealth = GetComponent<PlayerHealthManager>();
        anim = GetComponent<Animator>();

        grounded = true;

        playerForce = GetComponent<PlayerForce>();

        MapFinder findMap = FindObjectOfType<MapFinder>();
        if (findMap)
        {
            currentMap = findMap.currentMap;
            
            switch (currentMap)
            {
                case Maps.Map1:
                    jumpHitGroundVFX.GetComponent<VisualEffect>().SetGradient("GradientColor", darkerHitEffectGradient);
                    hitGroundVFX.GetComponent<VisualEffect>().SetGradient("GradientColor", darkerHitEffectGradient);
                    break;
                case Maps.Map2:
                    jumpHitGroundVFX.GetComponent<VisualEffect>().SetGradient("GradientColor", lightHitEffectGradient);
                    hitGroundVFX.GetComponent<VisualEffect>().SetGradient("GradientColor", lightHitEffectGradient);
                    break;
                case Maps.Map3:
                    jumpHitGroundVFX.GetComponent<VisualEffect>().SetGradient("GradientColor", darkerHitEffectGradient);
                    hitGroundVFX.GetComponent<VisualEffect>().SetGradient("GradientColor", darkerHitEffectGradient);
                    break;
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(isHit){
                GameObject vfx = Instantiate(hitGroundVFX, new Vector3(transform.position.x, 0, 0), Quaternion.identity);
                Destroy(vfx, 1f);
            }
            else
            {
                GameObject vfx = Instantiate(jumpHitGroundVFX, new Vector3(transform.position.x, 0, 0), Quaternion.identity);
                Destroy(vfx, 1f);
            }
            jumping = false;
            anim.SetBool("Grounded", true);
            grounded = true;
            currentForce = Vector2.zero;
            playerForce.AddToExisting(new Vector2(0, 0));
            setJumpCooldown = jumpCD;
        }
        else if(collision.gameObject.CompareTag("IgnoreColliders"))
        {
            playerForce.AddNormalForce(Vector2.zero, ForceMode.Impulse);
        }
        else if (collision.gameObject == otherPlayer.player)
        {
            if (anim.GetInteger("Attacks") <= 0)
            {
                playerForce.AddNormalForce(Vector2.zero, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            anim.SetBool("Grounded", false);

            if (!isHit && anim.GetInteger("Attacks") <= 0 && jumping)
            {
                anim.SetTrigger("Jump");
            }
        }
    }


    public void Walking(Vector2 directions)
    {
        if (playerManager.CanWalk)
        {
            if (anim.GetInteger("Attacks") <= 0 && !isHit)
            {
                if (directions != Vector2.zero)
                {
                    if (directions.x > .9f || directions.x < -.9f)
                    {
                        anim.SetBool("Walking", false);
                        anim.SetBool("Sprint", true);
                        moveSpeed = SprintSpeed;
                    }
                    else
                    {
                        anim.SetBool("Sprint", false);
                        anim.SetBool("Walking", true);
                        moveSpeed = defaultMoveSpeed;
                    }

                    if (directions.y < -.5f)
                    {
                        anim.SetBool("Walking", false);
                        anim.SetBool("Sit", true);
                    }
                    else
                    {
                        anim.SetBool("Sit", false);
                    }
                    anim.SetInteger("WalkDirection", playerManager.PlayerRotation * -1 * Mathf.RoundToInt(-directions.x));

                    if (directions.x * playerManager.PlayerRotation < 0 && grounded)
                    {
                        playerHealth.IsBlockingStandard = true;
                    }
                    else
                    {
                        playerHealth.IsBlockingStandard = false;
                    }
                    if (grounded)
                    {
                        if (directions.y > .4f && setJumpCooldown <= 0)
                        {
                            directions.y = jumpUp.y;
                            directions.x *= jumpUp.x;
                            jumping = true;
                        }
                        else
                        {
                            jumping = false;
                            directions.y = 0;
                        }
                    }

                    if (grounded)
                    {
                        currentForce = new Vector2(directions.x * moveSpeed * playerManager.PlayerRotation, directions.y);
                        playerForce.AddForce(currentForce, gravity, lerpJump, ForceMode.Impulse);
                    }
                }
            }
            else
            {
                anim.SetBool("Sprint", false);
                anim.SetBool("Walking", false);
            }
        }
        else
        {
            StopWalking();
            playerForce.ResetForce(Vector2.zero);
        }
    }

    public void StopWalking()
    {
        playerHealth.IsBlockingStandard = false;
        anim.SetBool("Sprint", false);
        anim.SetBool("Walking", false);
        anim.SetBool("Sit", false);
    }

    private void Update()
    {
        if (setJumpCooldown > -.9f && grounded)
        {
            setJumpCooldown -= Time.deltaTime;
        }
    }
}
