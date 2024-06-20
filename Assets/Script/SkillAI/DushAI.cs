using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DushAI : MonoBehaviour
{
    public float DushSpeed;
    public float DushCoolTime;
    public float DushTime;
    private PlayerHealth playerHealth;
    private PlayerControllerAI playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerAI>();
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
        playerController.AddPoints(5);
        Destroy(gameObject);
    }
}
