using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class TEST : Agent
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float jumpCooldown = 2f;

    private Rigidbody2D rb;
    private bool canJump = true;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // �ھڼҫ����ʧ@�Ӳ��ʩM���D����
        float moveInput = actions.ContinuousActions[0];
        bool jumpInput = actions.DiscreteActions[0] == 1;

        // ���k����
        Vector2 movement = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        // ���D����
        if (jumpInput && canJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
            StartCoroutine(ResetJumpCooldown());
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        var discreteActionsOut = actionsOut.DiscreteActions;

        // ���k���� (�s��ʧ@)
        continuousActionsOut[0] = Input.GetAxis("Horizontal");

        // ���D���� (�����ʧ@)
        discreteActionsOut[0] = Input.GetKeyDown(KeyCode.Space) ? 1 : 0;
    }

    private System.Collections.IEnumerator ResetJumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }

    public override void OnEpisodeBegin()
    {
        // ���s�]�m�����骺���A
        rb.velocity = Vector2.zero;
        transform.position = Vector3.zero;
        canJump = true;
    }
}
