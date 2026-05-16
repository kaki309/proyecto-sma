using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource uiSource;

    [Header("Test Clips")]
    [SerializeField] private AudioClip testMusicClip;
    [SerializeField] private AudioClip testSfxClip;
    [SerializeField] private AudioClip testUiClip;

    public void TestPlayMusic()
    {
        PlayMusic(testMusicClip);
    }

    public void TestPlaySfx()
    {
        PlaySfx(testSfxClip);
    }

    public void TestPlayUiSound()
    {
        PlayUiSound(testUiClip);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (musicClip == null) return;

        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySfx(AudioClip sfxClip)
    {
        if (sfxClip == null) return;

        sfxSource.PlayOneShot(sfxClip);
    }

    public void PlayUiSound(AudioClip uiClip)
    {
        if (uiClip == null) return;

        uiSource.PlayOneShot(uiClip);
    }

    public void StopAllAudio()
    {
        musicSource.Stop();
        sfxSource.Stop();
        uiSource.Stop();
    }
}
