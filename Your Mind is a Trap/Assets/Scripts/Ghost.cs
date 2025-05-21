using System;
using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Ghost : MonoBehaviour
{
    public Vector2 tendency = Vector2.left;
    private Rigidbody2D rb;
    private float wave;
    public float amp = 3f;
    private SpriteRenderer sprite;
    public Light2D sprite_light;
    private bool called = false;
    // Update is called once per frame
    void Start()
    {
        sprite_light = GetComponentInChildren<Light2D>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(GoTendency());
    }

    IEnumerator GoTendency() {
        for (int i = 0; i < 2000; i++) {
            RaycastHit up_info;
            Physics.Raycast(transform.position-Vector3.up*amp*(float)Math.Sin(wave), Vector3.up, out up_info);
            RaycastHit down_info;
            Physics.Raycast(transform.position-Vector3.up*amp*(float)Math.Sin(wave), Vector3.down, out down_info);
            rb.linearVelocity = tendency;
            if (3 > down_info.distance) {
                rb.linearVelocityY += 1.3f*tendency.magnitude;
            } else if (3.5 < down_info.distance) {
                rb.linearVelocityY -= 1.3f*tendency.magnitude;
            }
            wave += 0.15f;
            transform.position += new Vector3(0, amp*(float)Math.Sin(wave)-amp*(float)Math.Sin(wave-0.15f), 0);
            yield return new WaitForSeconds(0.02f);
        }
        Destroy(gameObject);
    }
    public IEnumerator GoesInvisAndDestroys() {
        if (!called) {
            called = true;
            print("called");
            for (int i = 0; i < 100; i++) {
                sprite.color -= new Color(0, 0, 0, 0.01f);
                sprite_light.color -= new Color(0, 0, 0, 0.01f);
                yield return new WaitForSeconds(0.05f);
            }
            Destroy(gameObject);
        }
    }
}
