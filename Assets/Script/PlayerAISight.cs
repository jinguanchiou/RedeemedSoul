using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAISight : MonoBehaviour
{
    private BoxCollider2D Sight;
    public float time;
    private float time_Log;
    public List<Transform> EnemyAttackTransforms { get; private set; }
    public List<Transform> SkillTransforms { get; private set; }
    void Start()
    {
        Sight = GetComponent<BoxCollider2D>();
        time_Log = time;
        EnemyAttackTransforms = new List<Transform>();
        SkillTransforms = new List<Transform>();
    }

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Sight.enabled = false;
            Sight.enabled = true;
            time = time_Log;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyAttack") && other.gameObject.layer == 14)
        {
            if (!EnemyAttackTransforms.Contains(other.gameObject.transform))
            {
                EnemyAttackTransforms.Add(other.gameObject.transform);
            }
        }
        if (other.gameObject.CompareTag("TriggeredSkill") || other.gameObject.CompareTag("BoolSkill"))
        {
            if (!SkillTransforms.Contains(other.gameObject.transform))
            {
                SkillTransforms.Add(other.gameObject.transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyAttack") && other.gameObject.layer == 14)
        {
            if (EnemyAttackTransforms.Contains(other.gameObject.transform))
            {
                EnemyAttackTransforms.Remove(other.gameObject.transform);
            }
        }
        if (other.gameObject.CompareTag("TriggeredSkill") || other.gameObject.CompareTag("BoolSkill"))
        {
            if (SkillTransforms.Contains(other.transform))
            {
                SkillTransforms.Remove(other.transform);
            }
        }
    }
}