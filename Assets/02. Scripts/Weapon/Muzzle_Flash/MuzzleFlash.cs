using System.Collections;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    [Header("Muzzle Flash Settings")]
    [SerializeField] private Sprite[] flashSprites;         // �������� ������ ��������Ʈ �迭
    [SerializeField] private float flashDuration = 0.2f;   // ª�� �����ְ� ������� �ð�

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // �ʱ⿡�� ��Ȱ��ȭ
    }

    public void ShowFlash()
    {
        if (flashSprites.Length == 0 || spriteRenderer == null)
            return;

        // ���� ��������Ʈ ����
        spriteRenderer.sprite = flashSprites[Random.Range(0, flashSprites.Length)];
        spriteRenderer.enabled = true;

        // ���� �ð� �� �ڵ����� ��Ȱ��ȭ
        StopAllCoroutines();
        StartCoroutine(HideFlashAfterDelay());
    }

    private IEnumerator HideFlashAfterDelay()
    {
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.enabled = false;
    }
}
