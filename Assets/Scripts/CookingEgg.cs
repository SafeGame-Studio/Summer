using UnityEngine;
using UnityEngine.UI;

public class CookingEgg : MonoBehaviour
{
    public Sprite[] sprites;
    Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeBack()
    {
        image.sprite = sprites[1];
    }
}
