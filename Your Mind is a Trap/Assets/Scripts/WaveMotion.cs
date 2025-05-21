using System;
using System.Collections;
using UnityEngine;

public class WaveMotion : MonoBehaviour
{
    public Vector3 wave_motion;

    void Start()
    {
        StartCoroutine(Wave());
    }
    IEnumerator Wave()
    {
        for (int i = 0;true; i++)
        {
            transform.position += wave_motion * (float)(Math.Sin((i+1)*0.08f) - Math.Sin(i*0.08f));
            yield return new WaitForSeconds(0.02f);
        }
    }
}
