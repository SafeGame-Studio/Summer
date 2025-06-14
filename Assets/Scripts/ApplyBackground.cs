using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApplyBackground : MonoBehaviour
{
    void Start()
    {
        // Находим компонент фона
        Image backgroundImage = GameObject.Find("Background").GetComponent<Image>();

        // Берём фон для текущей сцены из выбранного набора
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        backgroundImage.sprite = BackgroundSystem.CurrentBackgrounds[sceneIndex - 1];
    }
}