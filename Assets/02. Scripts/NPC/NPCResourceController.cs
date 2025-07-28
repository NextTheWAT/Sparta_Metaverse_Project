using UnityEngine;

public class NPCResourceController : ResourceController
{
    private NpcDialog npcDialog;
    NPCController nPCController;

    private void Awake()
    {
        base.Awake(); // ResourceController�� Awake ȣ��
        npcDialog = GetComponent<NpcDialog>();
        nPCController = GetComponent<NPCController>();
    }

    public override bool ChangeHealth(float change)
    {
        bool result = base.ChangeHealth(change); // ���� ü�� ��ȭ�� �־����� Ȯ��

        if (!result) return false; // ü�� ������ �����ٸ� Hit() ȣ�� ����

        if(npcDialog != null)
        {
            npcDialog.Hit(); // ü�� ���Ұ� ����� ��쿡�� ����
        }

        return true;
    }
    public override void Death()
    {
        nPCController.Death();
        GameManager.instance.HardStartGame();
    }
}
