using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public static MusicScript Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
