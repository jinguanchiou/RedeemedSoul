using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPig1_1 : EnemyMontherPig
{
    public ConversationFunction Level1_1_01;
    public bool OnLock;
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        OnLock = true;
        if (Level1_1_01.LevelAlreadyTold)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!OnLock)
        {
            base.Update();
            AttackAI();
        }
    }
    void AttackAI()
    {
        if (playerTransform != null)
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;
            if (distance >= AttackRadius)
            {
                AttackFlip();
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * 2 * Time.deltaTime);
            }
        }
    }
    void AttackFlip()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        if (direction.x > 0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (direction.x < -0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    
}
