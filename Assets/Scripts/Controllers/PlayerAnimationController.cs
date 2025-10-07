using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnMelee()
    {
        animator.SetTrigger(AnimationService.MELEE_ATTACK);
    }

    public void OnShoot()
    {
        animator.SetTrigger(AnimationService.SHOOT);
    }

    public void OnIsWalking(bool isWalking)
    {
        animator.SetBool(AnimationService.IS_WALKING, isWalking);
    }
}
