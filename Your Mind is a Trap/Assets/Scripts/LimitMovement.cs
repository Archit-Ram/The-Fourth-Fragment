using System;
using UnityEngine;

public class LimitMovement : MonoBehaviour
{
    public float x_min;
    public float x_max;
    public float y_min;
    public float y_max;
    public Transform follow;
    public float y_multiplier = 0.7f;
    public Vector3 offset;
    void Update()
    {
        transform.position = follow.position + offset;
        transform.position = new Vector3(Math.Clamp(transform.position.x, x_min, x_max), Math.Clamp(offset.y+(transform.position.y-offset.y)*y_multiplier, y_min, y_max), transform.position.z);
    }
}
