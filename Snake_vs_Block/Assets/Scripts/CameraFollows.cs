using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    public GameObject Player;
    public Transform Camera;
    
    public float camera_YOffset;    // ������ �� ��� Y
    public float camera_ZOffset;    // ������ �� ��� Z

    public float xAngle = 50f; // ���� ������� ������ �� ��� �

    void Start()
    {
        Camera.Rotate(xAngle, 0, 0);
    }

    void Update()
    {
        Camera.transform.position = new Vector3(0.0f,
            Player.transform.position.y + camera_YOffset,
            Player.transform.position.z + camera_ZOffset);
    }
}
