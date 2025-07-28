using System.Collections;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    [Header("Muzzle Flash Settings")]
    [SerializeField] private Sprite[] flashSprites;         // 랜덤으로 보여줄 스프라이트 배열
    [SerializeField] private float flashDuration = 0.2f;   // 짧게 보여주고 사라지는 시간

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // 초기에는 비활성화
    }

    public void ShowFlash()
    {
        if (flashSprites.Length == 0 || spriteRenderer == null)
            return;

        // 랜덤 스프라이트 선택
        spriteRenderer.sprite = flashSprites[Random.Range(0, flashSprites.Length)];
        spriteRenderer.enabled = true;

        // 일정 시간 후 자동으로 비활성화
        StopAllCoroutines();
        StartCoroutine(HideFlashAfterDelay());
    }

    private IEnumerator HideFlashAfterDelay()
    {
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.enabled = false;
    }
}
