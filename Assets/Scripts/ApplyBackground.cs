using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApplyBackground : MonoBehaviour
{
    void Start()
    {
        // ������� ��������� ����
        Image backgroundImage = GameObject.Find("Background").GetComponent<Image>();

        // ���� ��� ��� ������� ����� �� ���������� ������
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        backgroundImage.sprite = BackgroundSystem.CurrentBackgrounds[sceneIndex - 1];
    }
}