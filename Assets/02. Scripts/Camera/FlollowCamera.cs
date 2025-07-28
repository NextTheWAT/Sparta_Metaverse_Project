using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // ���� ���
    public Vector3 followOffset = new Vector3(0, 0, -10f); // �⺻ ī�޶� ������

    void Start()
    {
        // �ڵ����� "Player" �±׸� ���� ������Ʈ�� ã��
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
            else
                Debug.LogWarning("FollowCamera: 'Player' �±׸� ���� ������Ʈ�� ã�� �� �����ϴ�.");
        }

        if (target != null)
        {
            // �÷��̾� ��ġ �������� ī�޶� ��ġ�� ��� �̵�
            transform.position = target.position + followOffset;
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // �� ������ ���󰡱�
            transform.position = target.position + followOffset;
        }
    }
}
