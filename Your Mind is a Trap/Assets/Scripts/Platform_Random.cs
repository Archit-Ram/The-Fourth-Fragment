using System.Collections;
using UnityEngine;

public class platform_Random : MonoBehaviour
{
    public Vector2 max_size;
    public Vector2 min_size;
    private Vector2 platform_size;
    public LayerMask playerMask;
    public int[] choices = { 1 };
    private float timeout = -1f;
    private SpriteRenderer spriteRenderer;
    public float Speed;
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, min_size);
        Gizmos.DrawWireCube(transform.position, max_size);
    }
#endif
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        platform_size = transform.localScale;
    }
    void Update()
    {
        timeout -= Time.deltaTime;
        if (timeout <= 0)
        {
            if (Physics2D.OverlapBox(transform.position, max_size, transform.eulerAngles.z, playerMask) && !Physics2D.OverlapBox(transform.position, min_size, transform.eulerAngles.z, playerMask))
            {
                switch (choices[Random.Range(0, choices.Length - 1)])
                {
                    case 0:
                        timeout = 5;
                        break;
                    case 1:
                        StartCoroutine(scale_down(0));
                        timeout = 4;
                        break;
                    case 2:
                        StartCoroutine(scale_down(platform_size.x / 2));
                        timeout = 4;
                        break;
                    case 3:
                        StartCoroutine(scale_down(-platform_size.x / 2));
                        timeout = 4;
                        break;
                    case 4:
                        StartCoroutine(move(new Vector2(0.08f, 0f)));
                        timeout = 6;
                        break;
                    case 5:
                        StartCoroutine(move(new Vector2(-0.08f, 0)));
                        timeout = 6;
                        break;
                    case 6:
                        StartCoroutine(dissapear());
                        timeout = 6;
                        break;
                    case 7:
                        StartCoroutine(spin(90));
                        timeout = 6;
                        break;
                    case 8:
                        StartCoroutine(spin(-90));
                        timeout = 6;
                        break;
                    default:
                        break;
                }
            }
        }
    }
    IEnumerator spin(float angle)
    {
        yield return new WaitForSeconds(0.75f);
        float spin_part = angle / 10f;
        for (int i = 0; i < 10; i++)
        {
            transform.localEulerAngles += new Vector3(0, 0, spin_part);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 10; i++)
        {
            transform.localEulerAngles -= new Vector3(0, 0, spin_part);
            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator dissapear()
    {
        yield return new WaitForSeconds(0.75f);
        for (int i = 0; i < 10; i++)
        {
            spriteRenderer.color -= new Color(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(0.02f);
        }
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        GetComponent<BoxCollider2D>().enabled = true;
        for (int i = 0; i < 10; i++)
        {
            spriteRenderer.color += new Color(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator scale_down(float move)
    {
        float partition = (platform_size.x - 0.2f) / 70f;
        float move_partition = move / 70f;
        for (int i = 0; i < 70; i++)
        {
            transform.localScale = new Vector3(transform.localScale.x - partition, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(transform.position.x + move_partition, transform.position.y, transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.75f);
        for (int i = 0; i < 70; i++)
        {
            transform.localScale = new Vector3(transform.localScale.x + partition, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(transform.position.x - move_partition, transform.position.y, transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator move(Vector2 move)
    {
        for (int i = 0; i < 70; i++)
        {
            transform.position = new Vector3(transform.position.x + move.x, transform.position.y + move.y, transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 70; i++)
        {
            transform.position = new Vector3(transform.position.x - move.x, transform.position.y - move.y, transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
