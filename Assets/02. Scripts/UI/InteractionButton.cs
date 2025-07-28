using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameType
{
    EasyGame,
    TheStackGame
}

public class InteractionButton : MonoBehaviour
{
    [SerializeField] private GameObject button_anim;
    [SerializeField] private GameType gameType;

    private bool isPlayerInRange = false;

    private void Start()
    {
        button_anim.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            button_anim.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                switch (gameType)
                {
                    case GameType.EasyGame:
                        GameManager.instance.EasyStartGame();
                        break;
                    case GameType.TheStackGame:
                        SceneManager.LoadScene("TheStackScene");
                        break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            button_anim.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            button_anim.gameObject.SetActive(false);
        }
    }
}
