using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dush : MonoBehaviour
{
    public float DushSpeed;
    public float DushDistance;
    public float DushTime;
    private PlayerHealth playerHealth;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DushStart()
    {
        playerHealth.Shield(true);
        StartCoroutine(CountTime());
    }
    IEnumerator CountTime()
    {
        yield return new WaitForSeconds(DushTime);
        playerHealth.Shield(false);
    }
}
