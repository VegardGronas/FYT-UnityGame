using System.Collections.Generic;
using UnityEngine;

public enum AttackPosition
{
    Standard,
    Sit,
    Air
}

[CreateAssetMenu(fileName = "CharacterAttacks", menuName = "CharacterAttacks/CharacterSpellPrefabs", order = 2)]
public class CharacterAttacks : ScriptableObject
{
    public List<GameObject> VFX;
    public List<AudioClip> hitSound;
    public List<float> dmg;
    public List<int> reaction;
    public List<Vector2> lSpeed;
    public List<Vector2> lSpeedSelf;
    public List<Vector2> lerpTarget;
    public List<Vector2> force;
    public List<Vector2> selfForce;
    public List<BodyTransforms> bodyTransform;
    public List<ForceMode> forceMode;
    public List<ForceMode> selfForceMode;
    public List<AttackPosition> attackPosition;
    public List<Reaction> definedReaction;
}
