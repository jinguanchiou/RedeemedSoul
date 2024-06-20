using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillarList : MonoBehaviour
{
    public GameObject FirePillar;
    public float TimeLag;
    public float distance;
    public float DestroyTime;
    public int frequency;

    void Start()
    {
        StartCoroutine(GenerateFirePillars());
    }

    void Update()
    {
        DestroyTime -= Time.deltaTime;
        if (DestroyTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator GenerateFirePillars()
    {
        float direction = transform.rotation.eulerAngles.y == 180 ? -1 : 1;
        float startX = transform.position.x - direction * (frequency / 2.0f) * distance;
        for (int i = 0; i < frequency; i++)
        {
            yield return new WaitForSeconds(TimeLag);
            Instantiate(FirePillar, new Vector2(startX + direction * distance * i, transform.position.y), transform.rotation);
        }
    }
}
