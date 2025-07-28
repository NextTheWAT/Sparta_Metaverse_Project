using UnityEngine;

public class NpcLookAtPlayer : MonoBehaviour
{
    public Transform player; // 플레이어를 할당해줘야 함
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (player == null)
        {
            Debug.LogWarning("Player가 할당되지 않았습니다.");
        }
    }

    void Update()
    {
        if (player == null) return;

        // 왼쪽에 있으면 flipX true (왼쪽 보기), 오른쪽이면 false
        spriteRenderer.flipX = player.position.x < transform.position.x;
    }
}
