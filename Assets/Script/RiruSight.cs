using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiruSight : MonoBehaviour
{
    private Riru3DController Riru3D;
    private PolygonCollider2D Sight;
    public float time;
    private float time_Log;
    private bool hasTurn;
    public List<Transform> SkillTransforms { get; private set; }

    void Start()
    {
        Riru3D = GameObject.FindGameObjectWithTag("Riru3D").GetComponent<Riru3DController>();
        Sight = GetComponent<PolygonCollider2D>();
        time_Log = time;
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

        turnSihgt();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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
        if (other.gameObject.CompareTag("TriggeredSkill") || other.gameObject.CompareTag("BoolSkill"))
        {
            if (SkillTransforms.Contains(other.transform))
            {
                SkillTransforms.Remove(other.transform);
            }
        }
    }

    void turnSihgt()
    {
        if (Riru3D.hasMoved)
        {
            Quaternion turnRotation = Quaternion.Euler(0, 180, 0);
            transform.rotation = turnRotation;
            hasTurn = true;
        }
        else if (!Riru3D.hasMoved)
        {
            Quaternion turnRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = turnRotation;
            hasTurn = false;
        }
    }
}
