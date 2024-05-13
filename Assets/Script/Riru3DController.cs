using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riru3DController : MonoBehaviour
{
    private bool hasMoved = false;
    private Animator RiruAnim;


    // Start is called before the first frame update
    void Start()
    {

        RiruAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
