using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnergy : MonoBehaviour
{
    public Vector3 targetPosition; // The target position to move to
    public Vector3 initialScale = Vector3.one; // The initial scale of the sprite
    public Vector3 targetScale = Vector3.one * 2; // The target scale of the sprite
    public float moveDuration = 2f; // The duration of the move
    public float scaleDuration = 2f; // The duration of the scale
    private int Damege = 999;
    private Vector3 startPosition;
    private float moveTime;
    private float scaleTime;

    void Start()
    {
        startPosition = transform.position;
        transform.localScale = initialScale;
        GameManager.instance.audioManager.Play(2, "SwordEnergy", false);
    }

    void Update()
    {
        if (moveTime < moveDuration)
        {
            moveTime += Time.deltaTime;
            float moveProgress = moveTime / moveDuration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, moveProgress);
        }

        if (scaleTime < scaleDuration)
        {
            scaleTime += Time.deltaTime;
            float scaleProgress = scaleTime / scaleDuration;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, scaleProgress);
        }
        if (moveTime >= moveDuration && scaleTime >= scaleDuration)
        {
            GameManager.instance.audioManager.Stop(2);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.GetComponent<EnemyMonsterGhost>())
            {
                GameManager.instance.audioManager.Play(1, "seSlashHit", false);
                other.GetComponent<EnemyMonsterGhost>().TakeDamage(Damege);
                other.GetComponent<EnemyMonsterGhost>().PlayerHitMe();
            }
            if (other.GetComponent<EnemyMontherPig>())
            {
                GameManager.instance.audioManager.Play(1, "seSlashHit", false);
                other.GetComponent<EnemyMontherPig>().TakeDamage(Damege);
            }
        }
        if (other.gameObject.CompareTag("Riru"))
        {
            other.GetComponent<RiruAI>().TakeDamage(Damege);
        }
    }
}
