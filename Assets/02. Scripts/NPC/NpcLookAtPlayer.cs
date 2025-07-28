using UnityEngine;

public class NpcLookAtPlayer : MonoBehaviour
{
    public Transform player; // �÷��̾ �Ҵ������ ��
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (player == null)
        {
            Debug.LogWarning("Player�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }

    void Update()
    {
        if (player == null) return;

        // ���ʿ� ������ flipX true (���� ����), �������̸� false
        spriteRenderer.flipX = player.position.x < transform.position.x;
    }
}
