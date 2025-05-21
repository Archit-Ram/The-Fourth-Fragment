using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
public class CameraShake : MonoBehaviour
{
    public Transform camera_transform;
    private Rigidbody rb;
    [Range(0, 100)]
    public float magnitude;
    [Range(1, 20)]
    public int speed;
    void Start() {
        rb = GetComponent<Rigidbody>();
        //StartCoroutine(shake_position());
    }
    // Update is called once per frame
    void Update()
    {
    }


    IEnumerator shake_position() {
        Vector2 start, end = new Vector2();
        while (true) {
            start = end;
            float vel = (float)Math.Exp(rb.linearVelocity.magnitude/10f) - 1;
            end = new Vector2(UnityEngine.Random.Range(-magnitude*vel, magnitude*vel)/100f, UnityEngine.Random.Range(-magnitude*vel, magnitude*vel)/100f);
            //end = new Vector2(noise.pnoise(start, new Vector2(1, 1)), noise.pnoise(start + new Vector2(0.3f, 0.3f), new Vector2(1, 1)));
            for (float i = 0; i < speed; i++) {
                camera_transform.position = transform.position + new Vector3(Mathf.Lerp(start.x, end.x, i/speed), Mathf.Lerp(start.y, end.y, i/speed), -20);
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
}
