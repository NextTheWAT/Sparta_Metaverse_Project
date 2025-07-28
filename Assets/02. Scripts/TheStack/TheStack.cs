using UnityEngine;

public class TheStack : MonoBehaviour
{
    // Const Value
    private const float BoundSize = 3.5f; // ��� �⺻ �ʺ�      
    private const float MovingBoundsSize = 3f; // ����� �̵��� �� �ִ� �Ÿ� ũ��  
    private const float StackMovingSpeed = 5.0f; // ��ü ������ �ö󰡴� �ӵ� 
    private const float BlockMovingSpeed = 3.5f; // ����� �¿�� �����̴� �ӵ� 
    private const float ErrorMargin = 0.1f; // ���е� ���� ���� (�߸��� ����)       

    public GameObject originBlock = null; // ���� ��� ������

    private Vector3 prevBlockPosition; // ���� ����� ��ġ
    private Vector3 desiredPosition; // ��ü ������ ��ǥ ��ġ (�ε巯�� ī�޶� �̵���)
    private Vector3 stackBounds = new Vector2(BoundSize, BoundSize); // ���� ����� �ʺ�

    Transform lastBlock = null; // ���� ������ ������ ���
    float blockTransition = 0f; // ��� �̵��� ���� �ð� ���� ����
    float secondaryPosition = 0f; // ���� ��ġ (X �Ǵ� Z��)

    int stackCount = -1; // ������� ���� ��� ��
    public int Score { get { return stackCount; } }

    int comboCount = 0; // �������� ��Ȯ�� ����� ���� Ƚ��
    public int Combo { get { return comboCount; } }

    private int maxCombo = 0; // ���� ���� �������� ������ �޺� ��
    public int MaxCombo { get => maxCombo; }

    public Color prevColor;
    public Color nextColor;

    bool isMovingX = true;

    int bestScore = 0; // �ְ� ���� ���� ����
    public int BestScore { get => bestScore; }

    int bestCombo = 0; // �ְ� �޺� ���� ����
    public int BestCombo { get => bestCombo; }

    // PlayerPrefs ���� Ű
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

        // ���� �� ����� �� ���� ������ �������� ����
        prevColor = GetRandomColor();
        nextColor = GetRandomColor();

        prevBlockPosition = Vector3.down;
        // ù ��� ����
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
                // ���� ����
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

        // PingPong: �¿� �ݺ� �̵� ȿ�� ����
        float movePosition = Mathf.PingPong(blockTransition, BoundSize) - BoundSize / 2;

        if (isMovingX)
        {
            // X�� �̵� �� Z ����
            lastBlock.localPosition = new Vector3(movePosition * MovingBoundsSize, stackCount, secondaryPosition);
        }
        else
        {
            // Z�� �̵� �� X ����
            lastBlock.localPosition = new Vector3(secondaryPosition, stackCount, -movePosition * MovingBoundsSize);
        }
    }

    bool Spawn_Block()
    {
        // ���� ��� ��ġ ���� (���� ��� ��ġ�� ���� �񱳿� ����)
        if (lastBlock != null)
            prevBlockPosition = lastBlock.localPosition;

        GameObject newBlock = null;
        Transform newTrans = null;

        // �� ��� ����
        newBlock = Instantiate(originBlock);

        if (newBlock == null)
        {
            Debug.Log("NewBlock Instantiate Failed!");
            return false;
        }

        // �� ��Ͽ� ���� ����
        ColorChange(newBlock);

        newTrans = newBlock.transform;
        // �θ� ���� (���� �ȿ� ����� �ڽ����� ����)
        newTrans.parent = this.transform;
        // ��ġ ����: ���� ��� ���� ����
        newTrans.localPosition = prevBlockPosition + Vector3.up;
        // ȸ�� �ʱ�ȭ
        newTrans.localRotation = Quaternion.identity;
        // ��� ũ�� ����
        newTrans.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

        // ��� ���� ����
        stackCount++;

        // ī�޶�/������ �̵��� ��ǥ ��ġ ������Ʈ
        desiredPosition = Vector3.down * stackCount;
        // ��� �̵� �ʱ�ȭ
        blockTransition = 0f;
        // ������ ��� ����
        lastBlock = newTrans;

        Stack_UIManager.Instance.UpdateScore();
        return true;
    }

    Color GetRandomColor()
    {
        // 100 ~ 250 ������ ���� RGB ���� ����
        float r = Random.Range(100f, 250f) / 255f;
        float g = Random.Range(100f, 250f) / 255f;
        float b = Random.Range(100f, 250f) / 255f;

        return new Color(r, g, b);
    }


    void ColorChange(GameObject go)
    {
        // prevColor �� nextColor�� �ε巴�� ����
        // 0 ~ 1 ������ �� (10�� ��� ����)
        Color applyColor = Color.Lerp(prevColor, nextColor, (stackCount % 11) / 10f);

        // ��Ͽ� ����
        Renderer rn = go.GetComponent<Renderer>();

        if (rn == null)
        {
            Debug.Log("Renderer is NULL!");
            return;
        }

        rn.material.color = applyColor;

        // ī�޶� ������ ��Ϻ��� �ణ ��ο� ������ ����
        Camera.main.backgroundColor = applyColor - new Color(0.1f, 0.1f, 0.1f);

        // ���� ������ ������ ���������� ���ο� �� �غ�
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
            bool isNegativeNum = (deltaX < 0) ? true : false; // �������� ������� üũ

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

                // �߷����� ����(rubble) ����
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
            bool isNegativeNum = (deltaZ < 0) ? true : false; // �������� ������� üũ

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

                // �߷����� ����(rubble) ����
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
        comboCount++; // �޺� 1 ����

        // �ִ� �޺� ����
        if (comboCount > maxCombo)
            maxCombo = comboCount;

        // 5�޺����� ���� ȿ�� ����
        if ((comboCount % 5) == 0)
        {
            Debug.Log("5Combo Success!");
            // ��� ũ�� Ȯ��
            stackBounds += new Vector3(0.5f, 0.5f);
            // ��� ũ�� �ִ� ����
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
        // ���� ������ �ְ� �������� ���� ���
        if (bestScore < stackCount)
        {
            Debug.Log("�ְ� ���� ����");
            bestScore = stackCount; // �ְ� ���� ����
            bestCombo = maxCombo; // �ְ� �޺��� �Բ� ����

            // PlayerPrefs�� ���� �� ���� ���� �ѵ� ������
            PlayerPrefs.SetInt(BestScoreKey, bestScore);
            PlayerPrefs.SetInt(BestComboKey, bestCombo);
        }
    }
    void GameOverEffect()
    {
        int childCount = this.transform.childCount;

        // ���� �� ��Ϻ��� �ִ� 20���� ���������� ó��
        for (int i = 1; i < 20; i++)
        {
            // ó���� �ڽ��� ������ ����
            if (childCount < i)
                break;

            // �ڿ������� �ڽ� ��� ��������
            GameObject go =
                this.transform.GetChild(childCount - i).gameObject;

            // 'Rubble'�̶�� �̸��� ������Ʈ�� ����
            if (go.name.Equals("Rubble"))
                continue;

            // Rigidbody�� �߰��� �߷� �� ���� �ۿ� �����ϰ� ��
            Rigidbody rigid = go.AddComponent<Rigidbody>();

            // ���ʰ� �¿� �������� ������ ���� ����
            rigid.AddForce(
                (Vector3.up * Random.Range(0, 10f)
                 + Vector3.right * (Random.Range(0, 10f) - 5f))
                * 100f
            );
        }

    }
}