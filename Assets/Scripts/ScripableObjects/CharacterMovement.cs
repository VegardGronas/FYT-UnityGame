using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterMovement", menuName = "CharacterMovement/CharacterMovement", order = 1)]
public class CharacterMovement : ScriptableObject
{
    public GameObject visuals;
    public int visualsDestroyTime;
    public BodyTransforms bodyPart;
    public Vector2 velocity;
}
