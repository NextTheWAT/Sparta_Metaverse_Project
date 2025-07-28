using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToVillageButton : MonoBehaviour
{
    // 버튼에서 호출할 함수
    public void OnClickReturnToVillage()
    {
        GameManager.instance.GameOver();
        UIManager.Instance.ChangeState(UIState.Home);
        SceneManager.LoadScene("MainScene");
    }
}
