using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public float lettersPerSecond = 5f;
    public string[] dialogueLines;

    private int currentLine = 0;
    private Coroutine typingCoroutine;
    private bool isTyping = false;

    void Start()
    {
        dialoguePanel.SetActive(false); // Скрыть панель в начале
    }

    public void TriggerDialogue()
    {
        dialoguePanel.SetActive(true);
        currentLine = 0;
        ShowCurrentLine();
    }

    void ShowCurrentLine()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(dialogueLines[currentLine]));
    }

    IEnumerator TypeText(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        isTyping = false;
        typingCoroutine = null;
    }

    public void OnNextButton()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = dialogueLines[currentLine];
            isTyping = false;
        }
        else
        {
            currentLine++;
            if (currentLine < dialogueLines.Length)
                ShowCurrentLine();
            else
                EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false); // Скрыть панель после завершения
    }
}
