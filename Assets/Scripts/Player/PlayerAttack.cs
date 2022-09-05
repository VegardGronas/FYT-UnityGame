using UnityEngine;

public enum AttackButtonPattern
{
    Cross,
    Square,
    Circle,
    Triangle,
    CrossCircle,
    CircleTriangle,
    TriangleSquare,
    SquareCross
}

public class PlayerAttack : MonoBehaviour
{
    //REFERENCE TO OTHER SCRIPTS
    private Animator anim;
    private InputGame inputGame;
    //__________________

    //PRIVATE VARIABLES
    [SerializeField]
    private Transform block;
    //_______________________________

    private void Start()
    {
        anim = GetComponent<Animator>();
        inputGame = GetComponentInParent<InputGame>();
    }

    private void Update()
    {
        if (inputGame.AttackBtns != Vector2.zero)
        {
            AttackButtonInput();
        }
    }

    private void AttackButtonInput()
    {
        if (inputGame.AttackBtns == Vector2.left)
        {
            anim.SetInteger("Attacks", 4);
            anim.SetInteger("ComboAttack", 4);
        }
        else if (inputGame.AttackBtns == Vector2.right)
        {
            anim.SetInteger("Attacks", 2);
            anim.SetInteger("ComboAttack", 2);
        }
        else if (inputGame.AttackBtns == Vector2.up)
        {
            anim.SetInteger("Attacks", 3);
            anim.SetInteger("ComboAttack", 3);
        }
        else if (inputGame.AttackBtns == Vector2.down)
        {
            anim.SetInteger("Attacks", 1);
            anim.SetInteger("ComboAttack", 1);
        }
    }

    public void InstantiaeBlockVFX(GameObject spell, float destroyTimer)
    {
        GameObject visual = Instantiate(spell, block.position, Quaternion.identity);
        Destroy(visual, destroyTimer);
    }
}
