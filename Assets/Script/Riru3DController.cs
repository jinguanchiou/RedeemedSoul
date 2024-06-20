using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riru3DController : MonoBehaviour
{
    public bool hasMoved { get; private set; }
    public bool isWalk;
    public bool isIdle;
    public bool isJumpAttack;
    public bool isThrustAttack;
    public bool isDoubleAttack;
    public bool isBlocking;
    public bool isActioning;

    private Animator RiruAnim;


    // Start is called before the first frame update
    void Start()
    {
        hasMoved = false;
        RiruAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAnimStatus();
        
        
    }
    public void Walk(bool Walk)
    {
        RiruAnim.SetBool("Walk", Walk);
    }
    public void Enchant()
    {
        RiruAnim.SetTrigger("Enchant");
    }
    public void ThrustAttack()
    {
        RiruAnim.SetTrigger("ThrustAttack");
    }
    public void JunpAttack()
    {
        RiruAnim.SetTrigger("JunpAttack");
    }
    public void DoubleAttack()
    {
        RiruAnim.SetTrigger("DoubleAttack");
    }
    public void Block()
    {
        RiruAnim.SetTrigger("Block");
    }
    public void turn3D(Transform playerTransform, Transform transform2D)
    {
        
        if (playerTransform.position.x < transform2D.position.x && !hasMoved)
        {
            Quaternion turnRotation = Quaternion.Euler(0, 180, 0);
            transform.rotation = turnRotation;
            Vector3 targetPosition = transform.position - transform.right * 2;
            transform.position = targetPosition;
            hasMoved = true;
        }
        else if (playerTransform.position.x > transform2D.position.x && hasMoved)
        {
            Quaternion turnRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = turnRotation;
            Vector3 targetPosition = transform.position - transform.right * 2;
            transform.position = targetPosition;
            hasMoved = false;
        }
    }
    void CheckAnimStatus()
    {
        AnimatorStateInfo stateInfo = RiruAnim.GetCurrentAnimatorStateInfo(0);
        isWalk = RiruAnim.GetBool("Walk");
        if (stateInfo.IsName("Armature|Idle"))
            isIdle = true;
        else
            isIdle = false;
        if (stateInfo.IsName("Armature|DoubleAttack"))
            isDoubleAttack = true;
        else
            isDoubleAttack = false;
        if (stateInfo.IsName("Armature|JunpAttack_takeoff") || stateInfo.IsName("Armature|JunpAttack_ControlAir") || stateInfo.IsName("Armature|JunpAttack_Slash"))
            isJumpAttack = true;
        else
            isJumpAttack = false;
        if (stateInfo.IsName("Armature|ThrustAttack_Charged") || stateInfo.IsName("Armature|ThrustAttack"))
            isThrustAttack = true;
        else
            isThrustAttack = false;
        if (stateInfo.IsName("Armature|block"))
            isBlocking = true;
        else
            isBlocking = false;
        if (stateInfo.IsName("Armature|AppearanceAction"))
            isActioning = true;
        else
            isActioning = false;
    }
}
