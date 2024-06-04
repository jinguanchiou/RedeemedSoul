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
        // 根據模型的動作來移動和跳躍攻擊
        float moveInput = actions.ContinuousActions[0];
        bool jumpInput = actions.DiscreteActions[0] == 1;

        // 左右移動
        Vector2 movement = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        // 跳躍攻擊
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

        // 左右移動 (連續動作)
        continuousActionsOut[0] = Input.GetAxis("Horizontal");

        // 跳躍攻擊 (離散動作)
        discreteActionsOut[0] = Input.GetKeyDown(KeyCode.Space) ? 1 : 0;
    }

    private System.Collections.IEnumerator ResetJumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }

    public override void OnEpisodeBegin()
    {
        // 重新設置智能體的狀態
        rb.velocity = Vector2.zero;
        transform.position = Vector3.zero;
        canJump = true;
    }
}
