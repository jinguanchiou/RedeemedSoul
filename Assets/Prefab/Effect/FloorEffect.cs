using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorEffect : MonoBehaviour
{
    public float DestroyTime;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DestroyTime -= Time.deltaTime;
        if(DestroyTime <= 0)
        {
            StartCoroutine(WaitDestroy());
        }
    }
    IEnumerator WaitDestroy()
    {
        anim.SetTrigger("Destroy");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
