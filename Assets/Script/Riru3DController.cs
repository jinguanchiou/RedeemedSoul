using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riru3DController : MonoBehaviour
{
    public Transform player;
    private Animator RiruAnim;

    // Start is called before the first frame update
    void Start()
    {
        RiruAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.right, direction);
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
}
