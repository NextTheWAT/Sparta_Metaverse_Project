using UnityEngine;

public class TheStack : MonoBehaviour
{
    // Const Value
    private const float BoundSize = 3.5f; // 블록 기본 너비      
    private const float MovingBoundsSize = 3f; // 블록이 이동할 수 있는 거리 크기  
    private const float StackMovingSpeed = 5.0f; // 전체 스택이 올라가는 속도 
    private const float BlockMovingSpeed = 3.5f; // 블록이 좌우로 움직이는 속도 
    private const float ErrorMargin = 0.1f; // 정밀도 판정 기준 (잘릴지 여부)       

    public GameObject originBlock = null; // 원본 블록 프리팹

    private Vector3 prevBlockPosition; // 이전 블록의 위치
    private Vector3 desiredPosition; // 전체 스택의 목표 위치 (부드러운 카메라 이동용)
    private Vector3 stackBounds = new Vector2(BoundSize, BoundSize); // 현재 블록의 너비

    Transform lastBlock = null; // 현재 생성된 마지막 블록
    float blockTransition = 0f; // 블록 이동을 위한 시간 누적 변수
    float secondaryPosition = 0f; // 고정 위치 (X 또는 Z축)

    int stackCount = -1; // 현재까지 쌓은 블록 수
    public int Score { get { return stackCount; } }

    int comboCount = 0; // 연속으로 정확히 블록을 놓은 횟수
    public int Combo { get { return comboCount; } }

    private int maxCombo = 0; // 가장 많이 연속으로 성공한 콤보 수
    public int MaxCombo { get => maxCombo; }

    public Color prevColor;
    public Color nextColor;

    bool isMovingX = true;

    int bestScore = 0; // 최고 점수 저장 변수
    public int BestScore { get => bestScore; }

    int bestCombo = 0; // 최고 콤보 저장 변수
    public int BestCombo { get => bestCombo; }

    // PlayerPrefs 저장 키
    private const string BestScoreKey = "BestScore";
    private const string BestComboKey = "BestCombo";

    private bool isGameOver = false;



    void Start()
    {
        if (originBlock == null)
        {
            Debug.Log("OriginBlock is NULL");
            return;
        }

        // 시작 시 사용할 두 가지 색상을 랜덤으로 설정
        prevColor = GetRandomColor();
        nextColor = GetRandomColor();

        prevBlockPosition = Vector3.down;
        // 첫 블록 생성
        Spawn_Block();
    }


    public void Restart()
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        isGameOver = false;

        lastBlock = null;
        desiredPosition = Vector3.zero;
        stackBounds = new Vector3(BoundSize, BoundSize);

        stackCount = -1;
        isMovingX = true;
        blockTransition = 0f;
        secondaryPosition = 0f;

        comboCount = 0;
        maxCombo = 0;

        prevBlockPosition = Vector3.down;

        prevColor = GetRandomColor();
        nextColor = GetRandomColor();

        //Spawn_Block();
        Spawn_Block();
    }


    void Update()
    {
        if (isGameOver)
            return;


        if (Input.GetMouseButtonDown(0))
        {
            if (PlaceBlock())
            {
                Spawn_Block();
            }
            else
            {
                // 게임 오버
                Debug.Log("GameOver");
                UpdateScore();
                isGameOver = true;
                GameOverEffect();
                Stack_UIManager.Instance.SetScoreUI();
            }
        }

        MoveBlock();
        transform.position = Vector3.Lerp(transform.position, desiredPosition, StackMovingSpeed * Time.deltaTime);
    }


    void MoveBlock()
    {
        blockTransition += Time.deltaTime * BlockMovingSpeed;

        // PingPong: 좌우 반복 이동 효과 생성
        float movePosition = Mathf.PingPong(blockTransition, BoundSize) - BoundSize / 2;

        if (isMovingX)
        {
            // X축 이동 → Z 고정
            lastBlock.localPosition = new Vector3(movePosition * MovingBoundsSize, stackCount, secondaryPosition);
        }
        else
        {
            // Z축 이동 → X 고정
            lastBlock.localPosition = new Vector3(secondaryPosition, stackCount, -movePosition * MovingBoundsSize);
        }
    }

    bool Spawn_Block()
    {
        // 이전 블록 위치 저장 (다음 블록 위치와 정렬 비교에 사용됨)
        if (lastBlock != null)
            prevBlockPosition = lastBlock.localPosition;

        GameObject newBlock = null;
        Transform newTrans = null;

        // 새 블록 생성
        newBlock = Instantiate(originBlock);

        if (newBlock == null)
        {
            Debug.Log("NewBlock Instantiate Failed!");
            return false;
        }

        // 새 블록에 색상 적용
        ColorChange(newBlock);

        newTrans = newBlock.transform;
        // 부모 설정 (스택 안에 블록을 자식으로 넣음)
        newTrans.parent = this.transform;
        // 위치 설정: 이전 블록 위에 쌓임
        newTrans.localPosition = prevBlockPosition + Vector3.up;
        // 회전 초기화
        newTrans.localRotation = Quaternion.identity;
        // 블록 크기 설정
        newTrans.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

        // 블록 개수 증가
        stackCount++;

        // 카메라/스택이 이동할 목표 위치 업데이트
        desiredPosition = Vector3.down * stackCount;
        // 블록 이동 초기화
        blockTransition = 0f;
        // 마지막 블록 갱신
        lastBlock = newTrans;

        Stack_UIManager.Instance.UpdateScore();
        return true;
    }

    Color GetRandomColor()
    {
        // 100 ~ 250 범위의 밝은 RGB 값을 생성
        float r = Random.Range(100f, 250f) / 255f;
        float g = Random.Range(100f, 250f) / 255f;
        float b = Random.Range(100f, 250f) / 255f;

        return new Color(r, g, b);
    }


    void ColorChange(GameObject go)
    {
        // prevColor → nextColor로 부드럽게 보간
        // 0 ~ 1 사이의 값 (10개 블록 기준)
        Color applyColor = Color.Lerp(prevColor, nextColor, (stackCount % 11) / 10f);

        // 블록에 적용
        Renderer rn = go.GetComponent<Renderer>();

        if (rn == null)
        {
            Debug.Log("Renderer is NULL!");
            return;
        }

        rn.material.color = applyColor;

        // 카메라 배경색도 블록보다 약간 어두운 색으로 설정
        Camera.main.backgroundColor = applyColor - new Color(0.1f, 0.1f, 0.1f);

        // 만약 보간이 끝까지 도달했으면 새로운 색 준비
        if (applyColor.Equals(nextColor) == true)
        {
            prevColor = nextColor;
            nextColor = GetRandomColor();
        }
    }


    bool PlaceBlock()
    {
        Vector3 lastPosition = lastBlock.transform.localPosition;

        if (isMovingX)
        {
            float deltaX = prevBlockPosition.x - lastPosition.x;
            bool isNegativeNum = (deltaX < 0) ? true : false; // 왼쪽으로 벗어났는지 체크

            deltaX = Mathf.Abs(deltaX);
            if (deltaX > ErrorMargin)
            {
                stackBounds.x -= deltaX;
                if (stackBounds.x <= 0)
                {
                    return false;
                }

                float middle = (prevBlockPosition.x + lastPosition.x) / 2;
                lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

                Vector3 tempPosition = lastBlock.localPosition;
                tempPosition.x = middle;
                lastBlock.localPosition = lastPosition = tempPosition;

                // 잘려나갈 조각(rubble) 생성
                float rubbleHalfScale = deltaX / 2f;
                CreateRubble(
                    new Vector3(isNegativeNum
                            ? lastPosition.x + stackBounds.x / 2 + rubbleHalfScale
                            : lastPosition.x - stackBounds.x / 2 - rubbleHalfScale
                        , lastPosition.y
                        , lastPosition.z),
                    new Vector3(deltaX, 1, stackBounds.y)
                );
                comboCount = 0;
            }
            else
            {
                ComboCheck();
                lastBlock.localPosition = prevBlockPosition + Vector3.up;
            }
        }
        else
        {
            float deltaZ = prevBlockPosition.z - lastPosition.z;
            bool isNegativeNum = (deltaZ < 0) ? true : false; // 왼쪽으로 벗어났는지 체크

            deltaZ = Mathf.Abs(deltaZ);
            if (deltaZ > ErrorMargin)
            {
                stackBounds.y -= deltaZ;
                if (stackBounds.y <= 0)
                {
                    return false;
                }

                float middle = (prevBlockPosition.z + lastPosition.z) / 2;
                lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

                Vector3 tempPosition = lastBlock.localPosition;
                tempPosition.z = middle;
                lastBlock.localPosition = lastPosition = tempPosition;

                // 잘려나갈 조각(rubble) 생성
                float rubbleHalfScale = deltaZ / 2f;
                CreateRubble(
                    new Vector3(
                        lastPosition.x
                        , lastPosition.y
                        , isNegativeNum
                            ? lastPosition.z + stackBounds.y / 2 + rubbleHalfScale
                            : lastPosition.z - stackBounds.y / 2 - rubbleHalfScale),
                    new Vector3(stackBounds.x, 1, deltaZ)
                );
                comboCount = 0;
            }
            else
            {
                ComboCheck();
                lastBlock.localPosition = prevBlockPosition + Vector3.up;
            }
        }

        secondaryPosition = (isMovingX) ? lastBlock.localPosition.x : lastBlock.localPosition.z;

        return true;
    }
    void ComboCheck()
    {
        comboCount++; // 콤보 1 증가

        // 최대 콤보 갱신
        if (comboCount > maxCombo)
            maxCombo = comboCount;

        // 5콤보마다 보상 효과 적용
        if ((comboCount % 5) == 0)
        {
            Debug.Log("5Combo Success!");
            // 블록 크기 확대
            stackBounds += new Vector3(0.5f, 0.5f);
            // 블록 크기 최대 제한
            stackBounds.x =
                (stackBounds.x > BoundSize) ? BoundSize : stackBounds.x;
            stackBounds.y =
                (stackBounds.y > BoundSize) ? BoundSize : stackBounds.y;
        }
    }

    void CreateRubble(Vector3 pos, Vector3 scale)
    {
        GameObject go = Instantiate(lastBlock.gameObject);
        go.transform.parent = this.transform;

        go.transform.localPosition = pos;
        go.transform.localScale = scale;
        go.transform.localRotation = Quaternion.identity;

        go.AddComponent<Rigidbody>();
        go.name = "Rubble";
    }
    void UpdateScore()
    {
        // 현재 점수가 최고 점수보다 높을 경우
        if (bestScore < stackCount)
        {
            Debug.Log("최고 점수 갱신");
            bestScore = stackCount; // 최고 점수 갱신
            bestCombo = maxCombo; // 최고 콤보도 함께 갱신

            // PlayerPrefs에 저장 → 앱을 껐다 켜도 유지됨
            PlayerPrefs.SetInt(BestScoreKey, bestScore);
            PlayerPrefs.SetInt(BestComboKey, bestCombo);
        }
    }
    void GameOverEffect()
    {
        int childCount = this.transform.childCount;

        // 가장 위 블록부터 최대 20개를 순차적으로 처리
        for (int i = 1; i < 20; i++)
        {
            // 처리할 자식이 없으면 종료
            if (childCount < i)
                break;

            // 뒤에서부터 자식 블록 가져오기
            GameObject go =
                this.transform.GetChild(childCount - i).gameObject;

            // 'Rubble'이라는 이름의 오브젝트는 무시
            if (go.name.Equals("Rubble"))
                continue;

            // Rigidbody를 추가해 중력 및 물리 작용 가능하게 함
            Rigidbody rigid = go.AddComponent<Rigidbody>();

            // 위쪽과 좌우 방향으로 무작위 힘을 가함
            rigid.AddForce(
                (Vector3.up * Random.Range(0, 10f)
                 + Vector3.right * (Random.Range(0, 10f) - 5f))
                * 100f
            );
        }

    }
}