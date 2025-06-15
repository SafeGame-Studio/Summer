using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour
{
    public Image characterImage;

    // ��� ��������� 1
    public Sprite boy;

    // ��� ��������� 2
    public Sprite girl;

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);

        switch (selectedCharacter)
        {
            case 0:
                characterImage.sprite = boy; break;
            case 1:
                characterImage.sprite = girl; break;
        }
    }
}
