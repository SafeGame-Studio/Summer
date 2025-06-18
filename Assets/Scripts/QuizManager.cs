using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject dialogPanel; // Панель с сообщением
    [SerializeField] private TextMeshProUGUI dialogText; // Текст сообщения (TextMeshPro)
    [SerializeField] private Button[] answerButtons; // Кнопки ответов

    [Header("Quiz Settings")]
    [SerializeField] private int correctAnswerIndex; // Индекс правильного ответа (начинается с 0)
    [SerializeField] private string nextSceneName; // Имя сцены для перехода

    [Header("Messages")]
    [SerializeField] private string correctAnswerMessage = "Правильный ответ"; // Сообщение для правильного ответа
    [SerializeField] private string[] wrongAnswerMessages; // Массив сообщений для неправильных ответов

    private void Start()
    {
        dialogPanel.SetActive(false); // Скрываем панель сообщения при старте

        // Проверяем, что массив сообщений для неправильных ответов соответствует количеству кнопок
        if (wrongAnswerMessages.Length != answerButtons.Length)
        {
            Debug.LogWarning("Количество сообщений для неправильных ответов не совпадает с количеством кнопок!");
        }

        // Назначаем обработчики для всех кнопок
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int buttonIndex = i; // Локальная копия для замыкания
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(buttonIndex));
        }
    }

    private void OnAnswerSelected(int selectedIndex)
    {
        // Показываем панель с сообщением
        dialogPanel.SetActive(true);

        if (selectedIndex == correctAnswerIndex)
        {
            dialogText.text = correctAnswerMessage;
        }
        else
        {
            // Проверяем, есть ли сообщение для выбранного индекса
            if (selectedIndex >= 0 && selectedIndex < wrongAnswerMessages.Length)
            {
                dialogText.text = wrongAnswerMessages[selectedIndex];
            }
            else
            {
                dialogText.text = "Неправильно. Попробуйте еще раз."; // Резервное сообщение
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