using UnityEngine;

public class BGMusic : MonoBehaviour
{
    //Singleton

    public static BGMusic Instance;
    private bool _isMusicPlaying = true;
    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ToggleMusicOnOff()
    {
        if (_isMusicPlaying)
        {
            _audioSource.Pause();
            _isMusicPlaying = false;
        }
        else
        {
            _audioSource.Play();
            _isMusicPlaying = true;
        }
    }
}
