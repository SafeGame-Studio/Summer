using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public float lettersPerSecond = 5f;
    public string[] dialogueLines;

    private int currentLine = -1;
    private Coroutine typingCoroutine;
    private bool isTyping = false;

    public GameObject Zuzik;
    private Animator animator;
    void Start()
    {
        animator = Zuzik.GetComponent<Animator>();
        //dialoguePanel.SetActive(false); // Скрыть панель в начале
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
        if(currentLine == -1)
        {
            dialoguePanel.SetActive(true);
        }
        if (currentLine == -1)
        {
            Zuzik.SetActive(true);
        }
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
            else if (currentLine < dialogueLines.Length - 1)
            {
                animator.SetBool("Exit", true);
            }
                
            else
                EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false); // Скрыть панель после завершения
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }
}
