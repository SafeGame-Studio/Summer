using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundSystem : MonoBehaviour
{
    // �������� ����� ��� ���� ������
    public Sprite[] backgroundSet1; // ���� A, B, C
    public Sprite[] backgroundSet2; // ���� X, Y, Z

    // ����������� ������ �� ������� ����� �����
    public static Sprite[] CurrentBackgrounds;

    // ���������� ��� ������� �� ������ 1
    public void SelectSet1()
    {
        CurrentBackgrounds = backgroundSet1;
        LoadNextScene();
    }

    // ���������� ��� ������� �� ������ 2
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