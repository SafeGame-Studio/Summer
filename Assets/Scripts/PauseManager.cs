using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // Ссылка на PauseMenuPanel
    private bool isPaused = false;

    void Start()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false); // При старте скрываем панель
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Ставим игру на паузу
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // Возобновляем игру
            pausePanel.SetActive(false);
        }
    }

    public void ContinueGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
