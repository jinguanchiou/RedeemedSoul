using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyTime();
    }
    void DestroyTime()
    {
        StartCoroutine(DestroyTimeIE());
    }
    IEnumerator DestroyTimeIE()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
