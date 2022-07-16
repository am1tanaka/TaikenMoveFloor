using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    [Tooltip("目標座標"), SerializeField]
    Vector3[] wayPoints;
    [Tooltip("移動速度"), SerializeField]
    float speed = 3;

    Vector3 velocity;
    int index;
    static float StopDistance => 0.01f;

    void Awake()
    {
        SetMove(0);
    }

    void Update()
    {
        // 到着チェック
        transform.position += Time.deltaTime * velocity;
        Vector3 to = wayPoints[index] - transform.position;
        if (Vector3.Dot(to, velocity) <= 0)
        {
            NextMove();
        }
    }

    void NextMove()
    {
        if (wayPoints.Length > 1)
        {
            index = (index + 1) % wayPoints.Length;
            SetMove(index);
        }
    }

    void SetMove(int idx)
    {
        if (wayPoints.Length <= idx)
        {
            return;
        }

        Vector3 v = wayPoints[idx] - transform.position;
        if (v.magnitude < StopDistance)
        {
            // 到着していたら次の移動へ
            NextMove();
            return;
        }

        index = idx;
        velocity = speed * v.normalized;
    }
}
