using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundSystem : MonoBehaviour
{
    // Варианты фонов для двух кнопок
    public Sprite[] backgroundSet1; // Фоны A, B, C
    public Sprite[] backgroundSet2; // Фоны X, Y, Z

    // Статическая ссылка на текущий набор фонов
    public static Sprite[] CurrentBackgrounds;

    // Вызывается при нажатии на Кнопку 1
    public void SelectSet1()
    {
        CurrentBackgrounds = backgroundSet1;
        LoadNextScene();
    }

    // Вызывается при нажатии на Кнопку 2
    public void SelectSet2()
    {
        CurrentBackgrounds = backgroundSet2;
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}