using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // 따라갈 대상
    public Vector3 followOffset = new Vector3(0, 0, -10f); // 기본 카메라 오프셋

    void Start()
    {
        // 자동으로 "Player" 태그를 가진 오브젝트를 찾음
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
            else
                Debug.LogWarning("FollowCamera: 'Player' 태그를 가진 오브젝트를 찾을 수 없습니다.");
        }

        if (target != null)
        {
            // 플레이어 위치 기준으로 카메라 위치를 즉시 이동
            transform.position = target.position + followOffset;
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // 매 프레임 따라가기
            transform.position = target.position + followOffset;
        }
    }
}
