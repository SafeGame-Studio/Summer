using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject dialogPanel; // ������ � ����������
    [SerializeField] private TextMeshProUGUI dialogText; // ����� ��������� (TextMeshPro)
    [SerializeField] private Button[] answerButtons; // ������ �������

    [Header("Quiz Settings")]
    [SerializeField] private int correctAnswerIndex; // ������ ����������� ������ (���������� � 0)
    [SerializeField] private string nextSceneName; // ��� ����� ��� ��������

    [Header("Messages")]
    [SerializeField] private string correctAnswerMessage = "���������� �����"; // ��������� ��� ����������� ������
    [SerializeField] private string[] wrongAnswerMessages; // ������ ��������� ��� ������������ �������

    private void Start()
    {
        dialogPanel.SetActive(false); // �������� ������ ��������� ��� ������

        // ���������, ��� ������ ��������� ��� ������������ ������� ������������� ���������� ������
        if (wrongAnswerMessages.Length != answerButtons.Length)
        {
            Debug.LogWarning("���������� ��������� ��� ������������ ������� �� ��������� � ����������� ������!");
        }

        // ��������� ����������� ��� ���� ������
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int buttonIndex = i; // ��������� ����� ��� ���������
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(buttonIndex));
        }
    }

    private void OnAnswerSelected(int selectedIndex)
    {
        // ���������� ������ � ����������
        dialogPanel.SetActive(true);

        if (selectedIndex == correctAnswerIndex)
        {
            dialogText.text = correctAnswerMessage;
        }
        else
        {
            // ���������, ���� �� ��������� ��� ���������� �������
            if (selectedIndex >= 0 && selectedIndex < wrongAnswerMessages.Length)
            {
                dialogText.text = wrongAnswerMessages[selectedIndex];
            }
            else
            {
                dialogText.text = "�����������. ���������� ��� ���."; // ��������� ���������
            }
        }
    }

    void Update()
    {
        if (dialogText.text == correctAnswerMessage && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}