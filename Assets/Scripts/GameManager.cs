using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool IsGameStarted;

    void Start()
    {
        Time.timeScale = 0f;
        IsGameStarted = false;
    }

    void Update()
    {
        if (!IsGameStarted && Input.touchCount > 0)
        {
            Time.timeScale = 1f;
            IsGameStarted = true;
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
}
