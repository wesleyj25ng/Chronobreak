using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;
    private AudioSource audioSource;
    private float originalVolume;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            originalVolume = audioSource.volume;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PauseMusic()
    {
        if (audioSource != null)
        {
            audioSource.Pause();
        }
    }

    public void ContinueMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void AdjustVolume()
    {
        if (audioSource != null)
        {
            if (PauseMenu.GameIsPaused)
            {
                audioSource.volume = originalVolume * 0.3f; // Reduce the volume
            }
            else if (!PauseMenu.GameIsPaused)
            {
                audioSource.volume = originalVolume; // Restore the original volume
                
            }
        }
    }
}