using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchSkill : MonoBehaviour
{
    public float DestoryTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<TreasureBox>())
        {
            other.GetComponent<TreasureBox>().Appear();
        }
    }
    // Update is called once per frame
    void Update()
    {
        DestoryTime -= Time.deltaTime;
        if(DestoryTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
