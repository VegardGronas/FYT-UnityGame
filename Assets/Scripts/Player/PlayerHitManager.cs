using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitManager : MonoBehaviour
{
    //GETTING REFERENCE TO OTHER SCRIPTS
    public CharacterScripableObjects characterInfo;
    private ReactionsManager reactions;
    private PlayerHealthManager healthManager;
    private PlayerManager playerManager;
    private Animator anim;
    private PlayerForce playerForce;
    //_________________________________________

    //GAMEOBJECTS
    private GameObject[] otherPlayer;
    public GameObject player;
    //______________________
    public List<Transform> bodyTransform;

    //SERIALIZEFIELD
    [SerializeField]
    private float distance;
    [SerializeField]
    private bool gameTesting;
    [SerializeField]
    private bool blocked;
    //________________________

    //PRIVATE VARIABLES
    private float currentAttackDamage;
    private int currentBodyTransform;
    private int currentReaction;
    private bool hitDir;
    private Vector2 lerpSpeed;
    private Vector2 sendForce;
    private Vector2 lerpTarget;
    private GameObject visualEffect;
    private Transform otherPlayerHead;
    private Transform otherPlayerFeet;
    private Transform otherPlayerFront;
    private Transform otherPlayerBack;
    private Transform currentBodyTransfrom;
    private ForceMode currentForceMode;
    private AttackPosition currentAttackPosition;
    private Reaction defineRection;
    public GameObject soundPrefab;
    private AudioSource hitFXSound;

    //TEST
    public Vector2 blockForce;
    public Vector2 blockLerp;
    //________________________

    //GETTERS AND SETTERS
    public bool CanAttack { get; set; }
    public bool IsAttacking { get; set; }
    public int AttackStage { get; set; }
    public bool AttackAnimRunning { get; set; }
    //____________________________________________

    private void Start()
    {
        hitFXSound = soundPrefab.GetComponent<AudioSource>();
        playerForce = GetComponent<PlayerForce>();
        otherPlayer = GameObject.FindGameObjectsWithTag("Player");
        playerManager = GetComponent<PlayerManager>();
        anim = GetComponent<Animator>();
        foreach (GameObject go in otherPlayer)
        {
            if (go != gameObject)
            {
                player = go; 
                reactions = go.GetComponent<ReactionsManager>();
                healthManager = go.GetComponent<PlayerHealthManager>();
                otherPlayerHead = go.GetComponent<PlayerManager>().highestPoint;
                otherPlayerFeet = go.GetComponent<PlayerManager>().lowestPoint;
                otherPlayerFront = go.GetComponent<PlayerManager>().front;
                otherPlayerBack = go.GetComponent<PlayerManager>().back;
            }
        }

        //Ignoring eachothers collider, we only wanna use colliders for ground collision and to be able to use physics
        if (player != null)
        {
           //Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), player.GetComponent<Collider>());
            reactions.Enemy = characterInfo.name;
        }
        IsAttacking = false;
    }

    private void Update()
    {
        if (playerManager.CanAttack || gameTesting)
        {
            if (IsAttacking)
            {
                bool hit = Hit(otherPlayerHead.position, otherPlayerFeet.position, otherPlayerFront.position, otherPlayerBack.position, currentBodyTransfrom.position);
                if (currentAttackDamage > 0)
                {
                    if (hit)
                    {
                        switch (currentAttackPosition)
                        {
                            case AttackPosition.Standard:
                                if (healthManager.IsBlockingLow || healthManager.IsBlockingStandard)
                                {
                                    anim.SetInteger("Attacks", -1);
                                    playerForce.AddForce(new Vector2(10 * -playerManager.PlayerRotation, 0), Vector2.zero, new Vector2(.2f, 1), ForceMode.Impulse);
                                    healthManager.BlockAttack();
                                }
                                else if(!healthManager.IsDead)
                                {
                                    SendDamageToOtherPlayer(currentAttackPosition);
                                    if (soundPrefab)
                                    {
                                        SpawnSounObject(soundPrefab);
                                    }
                                    else
                                    {
                                        Debug.Log("Missing sound ");
                                    }
                                }
                                break;
                            case AttackPosition.Sit:
                                if (healthManager.IsBlockingLow)
                                {
                                    anim.SetInteger("Attacks", -1);
                                    playerForce.AddForce(new Vector2(10 * -playerManager.PlayerRotation, 0), Vector2.zero, new Vector2(.2f, 1), ForceMode.Impulse);
                                    healthManager.BlockAttack();
                                }
                                else if(!healthManager.IsDead)
                                {
                                    SendDamageToOtherPlayer(currentAttackPosition);
                                    if (soundPrefab)
                                    {
                                        SpawnSounObject(soundPrefab);
                                    }
                                    else
                                    {
                                        Debug.Log("Missing sound ");
                                    }
                                }
                                break;
                            case AttackPosition.Air:
                                //Maybe add some air reactions on hit 
                                if (healthManager.IsBlockingLow || healthManager.IsBlockingStandard)
                                {
                                    healthManager.BlockAttack();
                                }
                                else
                                {
                                    SendDamageToOtherPlayer(currentAttackPosition);
                                    if (soundPrefab)
                                    {
                                        SpawnSounObject(soundPrefab);
                                    }
                                    else
                                    {
                                        Debug.Log("Missing sound ");
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }
        
    }

    private void SpawnSounObject(GameObject soundPref)
    {
        GameObject sound = Instantiate(soundPref, bodyTransform[currentBodyTransform].position, Quaternion.identity);

        Destroy(sound, 1f);
    }

    public void SetUpAttack(float dmg, int reaction, Vector2 selfForce, Vector2 force, Vector2 lSpeed, Vector2 lSpeedSelf, Vector2 lTarget, GameObject visual, BodyTransforms bodyPart, ForceMode forceMode, ForceMode selfForcemode, AttackPosition atckPos, Reaction definedReaction)
    {
        lerpSpeed = lSpeed;
        currentAttackDamage = dmg;
        currentReaction = reaction;
        sendForce = force;
        currentForceMode = forceMode;
        currentAttackPosition = atckPos;
        defineRection = definedReaction;
        lerpTarget = lTarget;

        playerForce.AddForce(selfForce, lTarget, lSpeedSelf, selfForcemode);
        switch (bodyPart)
        {
            case BodyTransforms.LeftArm:
                currentBodyTransform = 0;
                break;
            case BodyTransforms.RightArm:
                currentBodyTransform = 1;
                break;
            case BodyTransforms.LeftLeg:
                currentBodyTransform = 2;
                break;
            case BodyTransforms.RightLeg:
                currentBodyTransform = 3;
                break;
        }

        if (visual != null)
        {
            visualEffect = visual;
        }
        else
        {
            Debug.Log("Visal effect for this attack is null");
        }

        currentBodyTransfrom = bodyTransform[currentBodyTransform];

        IsAttacking = true;
        AttackAnimRunning = true;
    }

    //Animation events lauches this function
    public void SetUpGeneralAttack(CharacterAttacks predefinedData)
    {
        lerpSpeed = Vector2.zero;
        if (predefinedData.VFX.Count > 0)
        {
            if (predefinedData.hitSound.Count > 0 && SoundManager.Instance)
            {
                hitFXSound.volume = SoundManager.Instance.FXVolumeSource.volume;
                hitFXSound.mute = SoundManager.Instance.FXVolumeSource.mute;
                hitFXSound.clip = predefinedData.hitSound[AttackStage];
            }
            else
            {
                soundPrefab = null;
                Debug.Log("This attack has no soundEffect");
            }
            SetUpAttack(
                predefinedData.dmg[AttackStage],
                predefinedData.reaction[AttackStage],
                predefinedData.selfForce[AttackStage],
                predefinedData.force[AttackStage],
                predefinedData.lSpeed[AttackStage],
                predefinedData.lSpeedSelf[AttackStage],
                predefinedData.lerpTarget[AttackStage],
                predefinedData.VFX[AttackStage],
                predefinedData.bodyTransform[AttackStage],
                predefinedData.forceMode[AttackStage],
                predefinedData.selfForceMode[AttackStage],
                predefinedData.attackPosition[AttackStage],
                predefinedData.definedReaction[AttackStage]
                );
        }
        else
        {
            Debug.Log("VFX = NULL Cuurent stage " + AttackStage);
        }
    }

    private void InstantiateVisualEffectOnHit(GameObject visual, Transform bodyPart)
    {
        GameObject vfx = Instantiate(visual, new Vector3(healthManager.gameObject.transform.position.x, bodyPart.position.y, bodyPart.position.z - 0.2f), Quaternion.Inverse(gameObject.transform.rotation));
        VisualEffectDefinedTimer timer = vfx.GetComponent<VisualEffectDefinedTimer>();
        if (timer)
        {
            Destroy(vfx, timer.destroyTimer);
        }
        else
        {
            Destroy(vfx, 0);
        }
    }

    private void SendDamageToOtherPlayer(AttackPosition attackPos)
    {
        switch (attackPos)
        {
            case AttackPosition.Standard:
                if (!anim.GetBool("Grounded"))
                {
                    healthManager.RecieveHitInfo(currentAttackDamage, sendForce, currentForceMode, lerpSpeed, new Vector2(lerpTarget.x, -1), currentAttackPosition);
                    reactions.Reactions(5, defineRection, currentAttackPosition);
                }
                else
                {
                    healthManager.RecieveHitInfo(currentAttackDamage, sendForce, currentForceMode, lerpSpeed, lerpTarget, currentAttackPosition);
                    reactions.Reactions(currentReaction, defineRection, currentAttackPosition);
                }
                break;
            case AttackPosition.Air:
                healthManager.RecieveHitInfo(currentAttackDamage, sendForce, currentForceMode, lerpSpeed, lerpTarget, currentAttackPosition);
                reactions.Reactions(currentReaction, defineRection, currentAttackPosition);
                break;
            case AttackPosition.Sit:
                healthManager.RecieveHitInfo(currentAttackDamage, sendForce, currentForceMode, lerpSpeed, lerpTarget, currentAttackPosition);
                reactions.Reactions(currentReaction, defineRection, currentAttackPosition);
                break;
        }
        if (visualEffect != null)
        {
            if (currentBodyTransfrom != null)
            {
                InstantiateVisualEffectOnHit(visualEffect, currentBodyTransfrom);
            }
        }
        IsAttacking = false;
        AttackAnimRunning = false;
    }

    private bool Hit(Vector3 top, Vector3 bot, Vector3 front, Vector3 back, Vector3 targetStart)
    {
        if (playerManager.PlayerRotation > 0)
        {
            hitDir = ((targetStart.y < top.y) && (targetStart.y > bot.y) && (targetStart.x > front.x) && (targetStart.x < back.x));
        }
        else if(playerManager.PlayerRotation < 0)
        {
            hitDir = ((targetStart.y < top.y) && (targetStart.y > bot.y) && (targetStart.x < front.x) && (targetStart.x > back.x));
        }
        return hitDir;
    }
}
