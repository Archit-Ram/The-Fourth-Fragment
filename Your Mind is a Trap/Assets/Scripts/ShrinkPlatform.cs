using UnityEngine;

public class ShrinkPlatform : MonoBehaviour
{
    public float Deaths;
    float size;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        size = transform.localScale.x;
    }
    public void Shrink()
    {
        if (transform.localScale.x > 0.2f)
        {
            transform.localScale -= new Vector3(0.2f*size,0,0);
        }
    }
}
