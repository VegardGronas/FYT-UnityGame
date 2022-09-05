using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToWorldSpace : MonoBehaviour
{
    private Camera cam;

    public Transform tool1;
    public Transform tool2;
    public Transform start;
    public Transform end;
    public Transform back;
    public Transform front;
    public float num;
    public GameObject instanCube;
    private GameObject cube;
    public float dire;
    private bool ret;

    private bool Betweeen(Vector3 A, Vector3 B, Vector3 Start, Vector3 End)
    {
        Vector3 ab = (B - A).normalized;
        Vector3 startEnd = (End - Start).normalized;

        return Vector3.Dot(ab, startEnd) < 0;
    }

    void Start()
    {
        cam = Camera.main;
        cube = Instantiate(instanCube);
    }

    private void Update()
    {
        Debug.DrawLine(front.position, back.position, Color.yellow);
        Debug.DrawLine(tool1.position, tool2.position, Color.yellow);
    }

    public bool Looking(Vector3 player1, Vector3 player2)
    {
        return Vector3.Dot(player1.normalized, player2.normalized) > 0;
    }

    private bool Hit(Vector3 top, Vector3 bot, Vector3 front, Vector3 targetStart, float dir)
    {
        if (dir > 0)
        {
            ret = ((targetStart.y < top.y) && (targetStart.y > bot.y) && (targetStart.x < front.x));
        }
        else if(dir < 0)
        {
            ret = ((targetStart.y < top.y) && (targetStart.y > bot.y) && (targetStart.x > front.x));
        }

        return ret;
    }

    void OnGUI()
    {
        Vector2 p1Pos = cam.ScreenToWorldPoint(new Vector3(tool1.position.x, tool1.position.y, cam.nearClipPlane / cam.fieldOfView));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Player 1 world space camera nearcliplane: " + p1Pos);
        GUILayout.EndArea();
    }
}
