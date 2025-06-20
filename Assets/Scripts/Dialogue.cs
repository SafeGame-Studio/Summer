using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    [TextArea]
    public string text;
}

public class Dialogue : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup dialogueGroup;              // Ссылка на CanvasGroup панели
    public TextMeshProUGUI dialogueText;           // Текст диалога
    public TextMeshProUGUI speakerNameText;        // Имя говорящего
    public GameObject clickToContinueIcon;         // Иконка "нажмите, чтобы продолжить" (по желанию)

    [Header("Диалоги")]
    public DialogueLine[] maleDialogue;
    public DialogueLine[] femaleDialogue;

    private DialogueLine[] currentDialogue;
    private int currentLine = -1;
    private Coroutine typingCoroutine;
    private bool isTyping = false;

    [Header("Опции")]
    public bool clickAnywhere = true;
    public float lettersPerSecond = 20f;

    private bool waitingForFirstClick = false;

    void Start()
    {
        HideDialogue();

        // Ждём первый клик для начала диалога
        waitingForFirstClick = true;

        int characterID = PlayerPrefs.GetInt("SelectedCharacter", 0);
        currentDialogue = (characterID == 1) ? femaleDialogue : maleDialogue;
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        if (!clickAnywhere) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (waitingForFirstClick)
            {
                waitingForFirstClick = false;
                TriggerDialogue(); // Показать панель и начать диалог
            }
            else if (dialogueGroup.alpha > 0f)
            {
                OnNextButton(); // Следующая реплика
            }
        }
    }

    public void TriggerDialogue()
    {
        if (currentDialogue.Length == 0)
        {
            EndDialogue();
            return;
        }

        ShowDialogue();
        currentLine = 0;
        ShowCurrentLine();
    }

    void ShowCurrentLine()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        DialogueLine line = currentDialogue[currentLine];
        speakerNameText.text = line.speakerName;

        typingCoroutine = StartCoroutine(TypeText(line.text));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        if (clickToContinueIcon != null)
            clickToContinueIcon.SetActive(false);

        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        isTyping = false;

        if (clickToContinueIcon != null)
            clickToContinueIcon.SetActive(true);
    }

    public void OnNextButton()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentDialogue[currentLine].text;
            isTyping = false;

            if (clickToContinueIcon != null)
                clickToContinueIcon.SetActive(true);
            return;
        }

        currentLine++;
        if (currentLine < currentDialogue.Length)
        {
            ShowCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        HideDialogue();
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }

    void ShowDialogue()
    {
        dialogueGroup.alpha = 1f;
        dialogueGroup.interactable = true;
        dialogueGroup.blocksRaycasts = true;
    }

    void HideDialogue()
    {
        dialogueGroup.alpha = 0f;
        dialogueGroup.interactable = false;
        dialogueGroup.blocksRaycasts = false;
    }
}
