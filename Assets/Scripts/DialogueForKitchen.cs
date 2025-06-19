using System.Collections;
using System.Security.Cryptography;
using TMPro;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DialogueForKitchen : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup dialogueGroup;              // Ссылка на CanvasGroup панели
    public TextMeshProUGUI dialogueText;           // Текст диалога
    public TextMeshProUGUI speakerNameText;        // Имя говорящего
    public GameObject clickToContinueIcon, gameObj, button, oil, egg, egg1, cap, eggInPan, eggInPan2;         // Иконка "нажмите, чтобы продолжить" (по желанию)

    [Header("Диалоги")]
    public DialogueLine[] maleDialogue;
    public DialogueLine[] femaleDialogue;

    private DialogueLine[] currentDialogue;
    private int currentLine = -1, countAction = 0, eggs, capActions;
    private Coroutine typingCoroutine;
    private bool isTyping = false, check = true, actionCompleted, addAction = true, eggsAdd;

    [Header("Опции")]
    public bool clickAnywhere = true;
    public float lettersPerSecond = 20f;

    private bool waitingForFirstClick = false;

    Animator animHot, animOil, animEgg, animCookingEgg, animCap;
    void Start()
    {
        animOil = oil.GetComponent<Animator>();
        animHot = gameObj.GetComponent<Animator>();
        animEgg = egg.GetComponent<Animator>();
        animCookingEgg = eggInPan.GetComponent<Animator>();
        animCap = cap.GetComponent<Animator>();

        HideDialogue();

        // Ждём первый клик для начала диалога
        waitingForFirstClick = true;

        int characterID = PlayerPrefs.GetInt("SelectedCharacter", 0);
        currentDialogue = (characterID == 1) ? femaleDialogue : maleDialogue;
    }

    void Update()
    {
        if (!clickAnywhere) return;

        if (Input.GetMouseButtonDown(0) && check)
        {
            actionCompleted = false;
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
        if (currentLine == 7 && actionCompleted)
        {
            countAction++;
            actionCompleted = false;
            StartCoroutine(WaitForFunction2());
        }
            
        if (currentLine == 2 || currentLine == 4 || currentLine == 5 || currentLine == 7 || currentLine == 8)
        {
            if (addAction)
            {
                addAction = false;
                countAction++;
            }
            if(!actionCompleted)
                check = false;
        }
        else if(actionCompleted)
        {
            check = true;
        }
        if (countAction == 7)
            StartCoroutine(Wait());
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

    public void OnOil()
    {
        if (countAction == 1)
        {
            if(isTyping)
            {
                OnNextButton();
                OnNextButton();
            }
            else
                OnNextButton();
            addAction = true;
            actionCompleted = true;
            animOil.enabled = true;
        }
    }
    public void OnPlate()
    {
        if (countAction == 2)
        {
            if (isTyping)
            {
                OnNextButton();
                OnNextButton();
            }
            else
                OnNextButton();
            addAction = true;
            actionCompleted = true;
            animHot.enabled = true;
            GameObject.Find("Lamp").GetComponent<Image>().enabled = true;
        }
        if(countAction == 6)
        {
            countAction++;
            animHot.SetBool("Off", true);
            GameObject.Find("Lamp").GetComponent<Image>().enabled = false;
        }
    }
    public void AddEgg()
    {
        if (countAction == 3)
        {
            if(eggs == 0)
            {
                animEgg.enabled = true;

                StartCoroutine(WaitForFunction());
            }
        }
    }
    public void Cap()
    {
        if (countAction == 4 && eggsAdd && currentLine == 7)
        {
            animCap.enabled = true;
            animCookingEgg.enabled = true;
            actionCompleted = true;
            eggInPan.SetActive(true);
            eggInPan2.SetActive(false);
            capActions = 1;
        }
        if (countAction == 7 && capActions == 1 && currentLine == 8)
        {
            animCap.SetBool("Open", true);
            countAction++;
        }
        
    }
    IEnumerator WaitForFunction()
    {
        eggs = 2;
        yield return new WaitForSeconds(2);
        addAction = true;
        egg1.SetActive(false);
        animEgg.SetInteger("Egg", 1);

        yield return new WaitForSeconds(2);

        actionCompleted = true;
        if (isTyping)
        {
            OnNextButton();
            OnNextButton();
        }
        else
            OnNextButton();
        eggsAdd = true;
    }
    IEnumerator WaitForFunction2()
    {
        yield return new WaitForSeconds(5);
        countAction++;
        OnNextButton();
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        OnNextButton();
    }
}

