using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    public Animator Anim;
    public bool AnimIsWorking;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimContorller();
    }
    void AnimContorller()
    {
        if(!AnimIsWorking)
        {
            AnimIsWorking = true;
            StartCoroutine(AnimContorllerIE());
        }
    }
    IEnumerator AnimContorllerIE()
    {
        Anim.SetTrigger("ToIdle2");
        yield return new WaitForSeconds(2);
        Anim.SetTrigger("ReturnToIdle");
        yield return new WaitForSeconds(5);
        AnimIsWorking = false;
    }
}
