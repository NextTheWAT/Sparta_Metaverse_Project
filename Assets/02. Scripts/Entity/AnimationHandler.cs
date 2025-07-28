using System.Collections;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsJump = Animator.StringToHash("IsJump");
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");

    public Animator animator;

    private Coroutine jumpCoroutine;
    private bool isHoldingJump = false;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void SetJump(bool isPressed)
    {
        if (isPressed)
        {
            if (!animator.GetBool(IsJump))
            {
                animator.SetBool(IsJump, true);
                if (jumpCoroutine != null) StopCoroutine(jumpCoroutine);
                jumpCoroutine = StartCoroutine(HoldJumpForMinDuration());
            }

            isHoldingJump = true;
        }
        else
        {
            isHoldingJump = false;
        }
    }

    private IEnumerator HoldJumpForMinDuration()
    {
        yield return new WaitForSeconds(0.3f); // �ּ� ���� �ð�
        while (!isHoldingJump) // ������ ���� �ʴٸ� �ٷ� false
        {
            animator.SetBool(IsJump, false);
            yield break;
        }

        // ������ �ִٸ� ����ϴٰ� ���� �� �ٽ� Ȯ��
        while (isHoldingJump)
        {
            yield return null;
        }

        animator.SetBool(IsJump, false);
    }

    public virtual void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f);
    }

    public virtual void InvincibilityEnd()
    {
        animator.SetBool(IsDamage, false);
    }

    public virtual void Damage()
    {
        Debug.Log("Damage Animation Triggered");
        animator.SetBool(IsDamage, true);
    }
}
