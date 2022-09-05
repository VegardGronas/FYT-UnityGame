using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationVisualEffect", menuName = "CharacterVisuals/AnimationVisuals", order = 1)]
public class VisualEffectOnAnimation : ScriptableObject
{
    public GameObject vfx;
    public int destroyTimer;
    public BodyTransforms bodyTransform;
    public Vector2 velocity;
}
