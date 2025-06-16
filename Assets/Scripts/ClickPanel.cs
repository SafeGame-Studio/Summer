using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UIElements.InputSystem;

public class ClickPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject dialogue;
    Dialogue diag;
    RectTransform rect;
    BoxCollider2D col;
    bool isOver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        diag = dialogue.GetComponent<Dialogue>();
        col = GetComponent<BoxCollider2D>();
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        col.size = new Vector2(rect.rect.width, rect.rect.height);
        if (Input.GetMouseButtonDown(0) && isOver)
        {
            diag.OnNextButton();
            if (SceneManager.GetActiveScene().buildIndex == 9)
            {
                GetComponent<CookingEgg>().ChangeBack();
            }
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
    }
}
