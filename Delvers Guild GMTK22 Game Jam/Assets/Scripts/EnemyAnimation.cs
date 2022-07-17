using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{

    private Animator eAnimator;
    int isGettingPushedHash, isAttackingHash, isDyingHash;

    public EnemyController eC;

    // Start is called before the first frame update
    void Start()
    {
        eAnimator = GetComponent<Animator>();
        isGettingPushedHash = Animator.StringToHash("isPushedBack");
        isAttackingHash = Animator.StringToHash("isAttacking");
        isDyingHash = Animator.StringToHash("isDying");
        eC = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(eAnimator != null)
        {
            //eAnimator.SetBool(isGettingPushedHash, eC.isPushedBack);
            //eAnimator.SetBool(isAttackingHash, eC.isAttacking);
            //eAnimator.SetBool(isDyingHash, eC.isDying);
        }
    }
}
