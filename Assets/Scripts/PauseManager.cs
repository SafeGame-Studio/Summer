using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // ������ �� PauseMenuPanel
    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false); // ��� ������ �������� ������
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // ������ ���� �� �����
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // ������������ ����
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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // � ����� ��������� ����
#endif
    }
}
