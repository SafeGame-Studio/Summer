using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    public void SelectCharacter(int id)
    {
        PlayerPrefs.SetInt("SelectedCharacter", id);
        SceneManager.LoadScene("MorningScene");
    }
}
