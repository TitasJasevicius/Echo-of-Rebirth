using UnityEngine;

public class AudioManager : MonoBehaviour
{
  [Header("Audio Sources")]
  [SerializeField] AudioSource audioSource;
  [SerializeField] AudioSource sfxSource;

  [Header("Audio Clips")]
  public AudioClip background;
  public AudioClip jump;

  private void Start()
  {
    audioSource.clip = background;
    audioSource.loop = true;
    audioSource.Play();
  }
  public void PlaySFX(AudioClip clip)
  {
    sfxSource.PlayOneShot(clip);
  }
}


