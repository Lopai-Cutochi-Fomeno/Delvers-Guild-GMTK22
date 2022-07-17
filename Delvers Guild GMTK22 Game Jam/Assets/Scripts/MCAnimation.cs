using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCAnimation : MonoBehaviour
{
    private Animator mAnimator;
    int isRunningHash, isJumpingHash, isFallingHash, isDyingHash;
    Rigidbody playerRb;
    public float stopIsJumping = 15f;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        isFallingHash = Animator.StringToHash("isFalling");
        isDyingHash = Animator.StringToHash("isDying");
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mAnimator != null)
        {
            bool hrzntlMvmntPressed = (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A));
            bool isRunning = mAnimator.GetBool(isRunningHash);
            bool isFalling = mAnimator.GetBool(isFallingHash);
            bool isJumping = mAnimator.GetBool(isJumpingHash);

            if (hrzntlMvmntPressed && !isRunning)
            {
                mAnimator.SetBool(isRunningHash, true);
            }
            if (!hrzntlMvmntPressed && isRunning)
            {
                mAnimator.SetBool(isRunningHash, false);
            }

            bool playerCanJump = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().canJump;

            mAnimator.SetBool(isJumpingHash, playerRb.velocity.y > stopIsJumping && !playerCanJump);

            mAnimator.SetBool(isFallingHash, playerRb.velocity.y < stopIsJumping+0.2f && !playerCanJump);

            mAnimator.SetBool(isDyingHash, GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().health <= 0);
        }
    }
}
