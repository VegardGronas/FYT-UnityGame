using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisualEffects : MonoBehaviour
{
    //0 = Leftarm, 1 = rightArm, 2 = leftLeg, 3 = rightLeg
    [SerializeField]
    private PlayerHitManager bodyTransforms;
    private PlayerManager pm;
    private int currentBodypart;

    private void Start()
    {
        bodyTransforms = GetComponent<PlayerHitManager>();
        pm = GetComponent<PlayerManager>();
    }

    public void InstantiateVisuals(VisualEffectOnAnimation vfxInfo)
    {
        switch (vfxInfo.bodyTransform)
        {
            case BodyTransforms.LeftArm:
                currentBodypart = 0;
                break;
            case BodyTransforms.RightArm:
                currentBodypart = 1;

                break;
            case BodyTransforms.LeftLeg:
                currentBodypart = 2;

                break;
            case BodyTransforms.RightLeg:
                currentBodypart = 3;

                break;
        }
        GameObject vfx = Instantiate(vfxInfo.vfx, bodyTransforms.bodyTransform[currentBodypart].position, Quaternion.Euler(0, -90 * pm.PlayerRotation, 0));
        vfx.AddComponent<ThrowingAttacks>();
        vfx.GetComponent<ThrowingAttacks>().velocity = vfxInfo.velocity * pm.PlayerRotation;
        Destroy(vfx, vfxInfo.destroyTimer);
    }

    public void ChildVisualEffects(VisualEffectOnAnimation predefinedVisual)
    {
        switch (predefinedVisual.bodyTransform)
        {
            case BodyTransforms.LeftArm:
                currentBodypart = 0;
                break;
            case BodyTransforms.RightArm:
                currentBodypart = 1;
                break;
            case BodyTransforms.LeftLeg:
                currentBodypart = 2;
                break;
            case BodyTransforms.RightLeg:
                currentBodypart = 3;
                break;
        }
        GameObject vfx = Instantiate(predefinedVisual.vfx, bodyTransforms.bodyTransform[currentBodypart].position, Quaternion.identity);
        vfx.transform.SetParent(bodyTransforms.bodyTransform[currentBodypart]);
        Destroy(vfx, predefinedVisual.destroyTimer);
    }
}
