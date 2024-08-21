using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    public Transform SnakeHead;
    private float diameter = 0.5f;

    private List<Transform> snakeBodies = new List<Transform>();
    private List<Vector3> positions = new List<Vector3>();

    private void Awake()
    {
        positions.Add(SnakeHead.position);
    }

    private void Update()
    {
        float distance = ((Vector3)SnakeHead.position - positions[0]).magnitude;

        if (distance > diameter)
        {
            Vector3 direction = ((Vector3)SnakeHead.position - positions[0]).normalized;

            positions.Insert(0, positions[0] + direction * diameter);
            positions.RemoveAt(positions.Count - 1);

            distance -= diameter;
        }

        for (int i = 0; i < snakeBodies.Count; i++)
        {
            snakeBodies[i].transform.position = Vector3.Lerp(positions[i + 1], positions[i], distance / diameter);
        }
    }

    public void AddBody()
    {
        Transform body = Instantiate(SnakeHead, positions[positions.Count - 1], Quaternion.identity, transform);
        snakeBodies.Add(body);
        positions.Add(body.position);
    }

    public void RemoveBody()
    {
        Destroy(snakeBodies[0].gameObject);
        snakeBodies.RemoveAt(0);
        positions.RemoveAt(1);
    }
}