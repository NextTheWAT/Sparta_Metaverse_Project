using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    public Camera camera;
    private GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        camera = Camera.main;
    }

    [SerializeField]
    private float playerSpeed = 3.0f;
    bool isRunning = false; // 플레이어가 달리고 있는지 여부


    [SerializeField] private AnimationHandler animationHandler;


    protected virtual void Awake()
    {
        base.Awake();
        animationHandler = GetComponent<AnimationHandler>();

        if (camera == null)
            camera = Camera.main;
    }

    public override void Death()
    {
        base.Death();
        gameManager.GameOver();
    }

    void OnMove(InputValue inputValue)
    {
        // 입력된 방향 값을 벡터로 받아옴 (WASD 등)
        movementDirection = inputValue.Get<Vector2>();
        // 대각선 이동 시 속도 일정하게 유지하기 위해 정규화
        movementDirection = movementDirection.normalized;
        movementDirection *= playerSpeed;


        if (weaponHandler != null)
        {
            if (movementDirection != Vector2.zero)
            {
                weaponHandler.Move(); // 이동 중이면 애니메이션 켬+
            }
            else
            {
                weaponHandler.animator.SetBool("IsMove", false); // 이동 멈추면 애니메이션 끔
            }
        }
    }

    void OnLook(InputValue inputValue)
    {
        // 마우스 포인터의 스크린 좌표를 가져옴
        Vector2 mousePosition = inputValue.Get<Vector2>();
        // 스크린 좌표를 월드 좌표로 변환
        Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);
        // 현재 위치와 마우스 위치 사이의 방향 벡터 계산
        lookDirection = (worldPos - (Vector2)transform.position);

        // 너무 가까우면 방향을 무시 (회전하지 않도록)
        if (lookDirection.magnitude < .9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            // 방향 벡터를 정규화해서 방향만 유지
            lookDirection = lookDirection.normalized;
        }
    }
    void OnJump(InputValue inputValue)
    {
        animationHandler.SetJump(inputValue.isPressed);
    }

    void OnFire(InputValue inputValue)
    {
        // UI 요소 위에서 클릭한 경우 공격 무시
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // 버튼을 누르고 있는지 여부로 공격 상태 설정
        isAttacking = inputValue.isPressed;
    }

}
