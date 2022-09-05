using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomCamera
{
    public struct CustomCameraClass
    {
        public static Vector3 GetCenter(Vector3 obj1, Vector3 obj2, float offsetZ, float offsetY)
        {
            float y = (obj1.y + obj2.y) / 2;
            if (y > 0)
            {
                y = -y;
            }
            float offY = offsetY - y;
            float offZ = offsetZ + y;
            Vector3 center = new((obj1.x + obj2.x) / 2, offY, offZ);
            return center;
        }

        public static bool InBetweenVectors(Vector3 A, Vector3 B, Vector3 start, Vector3 end)
        {
            Vector3 startEnd = (end - start).normalized;
            Vector3 ba = (B - A).normalized;

            Debug.Log(startEnd);

            return Vector3.Dot(ba, startEnd) < 0 && Vector3.Dot(startEnd, ba) < 0;
        }
    }

    /*
     * Denne klassen er skrevet mest for å lære mer om C#, hvordan språket fungere utenfor standard monobehaviour klasser. Denne scripten er den eneste av denne typen i spillet.
     * 
     * 
     * https://docs.unity3d.com/ScriptReference/Vector3.Dot.html
     * using UnityEngine;
using System.Collections;
                                                    .Dot((Lowest - Highest).normalized, (C - B).normalized) < 0f  && Vector3.Dot((A - B).normalized, (C - A).normalized) < 0f;
                                                   
public class ExampleClass : MonoBehaviour
{
    public Transform other;

    void Update()
    {
        if (other)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 toOther = other.position - transform.position;

            if (Vector3.Dot(forward, toOther) < 0)
            {
                print("The other transform is behind me!");
            }
        }
    }
}
     * 
     * */
}

