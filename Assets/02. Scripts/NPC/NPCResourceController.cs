using UnityEngine;

public class NPCResourceController : ResourceController
{
    private NpcDialog npcDialog;
    NPCController nPCController;

    private void Awake()
    {
        base.Awake(); // ResourceController의 Awake 호출
        npcDialog = GetComponent<NpcDialog>();
        nPCController = GetComponent<NPCController>();
    }

    public override bool ChangeHealth(float change)
    {
        bool result = base.ChangeHealth(change); // 실제 체력 변화가 있었는지 확인

        if (!result) return false; // 체력 변경이 없었다면 Hit() 호출 안함

        if(npcDialog != null)
        {
            npcDialog.Hit(); // 체력 감소가 적용된 경우에만 실행
        }

        return true;
    }
    public override void Death()
    {
        nPCController.Death();
        GameManager.instance.HardStartGame();
    }
}
