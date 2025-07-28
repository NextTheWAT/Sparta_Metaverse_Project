using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToVillageButton : MonoBehaviour
{
    // ��ư���� ȣ���� �Լ�
    public void OnClickReturnToVillage()
    {
        GameManager.instance.GameOver();
        UIManager.Instance.ChangeState(UIState.Home);
        SceneManager.LoadScene("MainScene");
    }
}
