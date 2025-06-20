using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public static MusicScript Instance;
    public AudioSource audio;
    private void Awake()
    {
        if (Instance == null)
        {
            audio.volume = 0.05f;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
