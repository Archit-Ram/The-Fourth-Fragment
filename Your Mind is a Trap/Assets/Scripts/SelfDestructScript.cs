using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructScript2 : MonoBehaviour
{
    public float TimeToDestruct;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct(TimeToDestruct));
    }

    // Update is called once per frame
    IEnumerator SelfDestruct(float TimeToDestroy)
    {
        yield return new WaitForSeconds(TimeToDestroy);
        Destroy(gameObject);
    }
}
