using UnityEngine;
using System.Collections;

public class CatJump : MonoBehaviour
{
    public float jumpMoment;
    public float jumpPower;

    public CatAnimationController cac;
    public CharacterController cc;

    public bool isJumping
    {
        get
        {
            return false;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            TryJump();
        }
        if (cc.isGrounded)
        {
            TryInterruptJump();
        }
    }
    
    void TryInterruptJump()
    {

    }

    void TryJump()
    {

    }

    void JumpStart()
    {

    }

    void Jump()
    {

    }

    void JumpEnd()
    {

    }


}
