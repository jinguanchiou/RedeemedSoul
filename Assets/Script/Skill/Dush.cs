using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dush : MonoBehaviour
{
    public float DushSpeed;
    public float DushCoolTime;
    public float DushTime;
    private PlayerHealth playerHealth;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        DushStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DushStart()
    {
        playerController.teleportationSkill(DushSpeed, DushTime, DushCoolTime);
        StartCoroutine(DushDestroy());
    }
    IEnumerator DushDestroy()
    {
        yield return new WaitForSeconds(DushTime);
        Destroy(gameObject);
    }
}
