using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    public float IntervalTime;
    public float DestroyTime;
    private bool isAttacking;
    private PolygonCollider2D polygon;
    // Start is called before the first frame update
    void Start()
    {
        polygon = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DestroyTime -= Time.deltaTime;
        if(!isAttacking)
        {
            isAttacking = true;
            polygon.enabled = true;
            StartCoroutine(PoisonIE());
        }
        if(DestroyTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator PoisonIE()
    {
        yield return new WaitForSeconds(IntervalTime);
        isAttacking = false;
        polygon.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            other.GetComponent<PlayerHealth>().PoisonPlayer();
        }
    }
}
