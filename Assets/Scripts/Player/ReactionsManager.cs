using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Reaction
{
    General,
    Spesific
}

public class ReactionsManager : MonoBehaviour
{
    //REFERENCE TO OTHER SCRIPTS
    private Animator anim;
    //____________________________

    //PUBLIC VARIABLES
    public string Enemy { get; set; }
    //____________________

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Reactions(int react, Reaction definedReaction, AttackPosition attackPos)
    {
        switch (definedReaction)
        {
            case Reaction.General:
                if (attackPos == AttackPosition.Air)
                {
                    //have some predefiend animations for air attacks
                }
                anim.SetInteger("GeneralReactions", react);
                break;
            case Reaction.Spesific:
                if (attackPos == AttackPosition.Air)
                {
                    //have some spsific predefiend animations for air attacks
                }
                anim.SetInteger("Reactions", react);
                break;
        }
    }

    private void Update()
    {
        if (Enemy != "")
        {
            //FIks dette
            anim.SetBool(Enemy, true);
        }
    }
}
