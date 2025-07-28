using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static NPCState;

public class NpcDialog : MonoBehaviour
{
    AnimationHandler animationHandler;
    public TextMeshProUGUI dialogText;
    public float chatInterval = 4f;
    private float lastHitTime;
    private float timer;

    private string[] idleChats = {
        "안녕!", "오늘 날씨 좋다~", "여기 처음 와봤어?", "너 게임 좋아해?", "미니게임할래?", "마을 주변을 잘 돌아다녀봐!",
        "미니게임을 잘 찾아봐!"
    };

    private string[] hitChats = {
        "어잇?!", "뭐야 지금!", "너 왜 그래!", "아야!", "총 맞았어!!"
    };

    private string[] angryChats = {
        "다신 보지 말자!", "너 때문에 망했어!", "화났다 진짜!", "이건 복수야...", "얼굴도 보기 싫어!"
    };

    private bool isHit = false;

    void Start()
    {
        animationHandler = GetComponent<AnimationHandler>();

        if (NPCState.moodState == NPCMoodState.Angry)
        {
            ShowRandomAngryChat();
        }
        else if(NPCState.moodState == NPCMoodState.Idle)
        {
            ShowRandomIdleChat();
        }
    }

    void Update()
    {
        if (isHit) return;

        timer += Time.deltaTime;
        if (timer >= chatInterval)
        {
            if (NPCState.moodState == NPCMoodState.Angry)
            {
                ShowRandomAngryChat();
            }
            else if (NPCState.moodState == NPCMoodState.Idle)
            {
                ShowRandomIdleChat();
            }

            timer = 0;
        }
    }

    void ShowRandomIdleChat()
    {
        if (dialogText == null) return;
        int index = Random.Range(0, idleChats.Length);
        dialogText.text = idleChats[index];
    }

    void ShowRandomHitChat()
    {
        if (dialogText == null) return;
        int index = Random.Range(0, hitChats.Length);
        dialogText.text = hitChats[index];
    }

    void ShowRandomAngryChat()
    {
        if (dialogText == null) return;
        int index = Random.Range(0, angryChats.Length);
        dialogText.text = angryChats[index];
    }

    public void Hit()
    {
        animationHandler.Damage();

        // 화남 상태일 땐 Hit 호출 무시
        if (NPCState.moodState == NPCMoodState.Angry)
            return;

        isHit = true;
        lastHitTime = Time.time;

        ShowRandomHitChat();
        Invoke(nameof(ResetHit), 3f);
    }

    void ResetHit()
    {
        if (Time.time - lastHitTime < 3f) return;
        isHit = false;
        timer = 0;
        ShowRandomIdleChat();
    }

    public void ShowInteractionHint(string message, float duration = 3f)
    {
        if (dialogText == null) return;

        CancelInvoke(nameof(ResetHit)); // 기존 히트 메시지 리셋 중이면 취소
        isHit = true;
        dialogText.text = message;

        Invoke(nameof(ResetInteractionHint), duration);
    }

    void ResetInteractionHint()
    {
        isHit = false;
        timer = 0;
        ShowRandomIdleChat();
    }
}
