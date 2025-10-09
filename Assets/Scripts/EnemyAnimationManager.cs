using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimationManager : MonoBehaviour
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

    public void OnIsWalking(bool isWalking)
    {
        animator.SetBool(AnimationService.IS_WALKING, isWalking);
    }

    public void OnDeath()
    {
        animator.SetTrigger(AnimationService.DEATH);
    }
}
