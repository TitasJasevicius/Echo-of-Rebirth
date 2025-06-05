using UnityEngine;

public class AudioManager : MonoBehaviour
{
  [Header("Audio Sources")]
  [SerializeField] AudioSource audioSource;
  [SerializeField] AudioSource sfxSource;

  [Header("Audio Clips")]
  public AudioClip background;
  public AudioClip bacgroundIntense;
  public AudioClip jump;
  public AudioClip purchaseItem;
  public AudioClip coinPicup;
  public AudioClip death;
  public AudioClip dash;
  public AudioClip attack;
  public AudioClip explosion;
  public AudioClip jump2;

  private void Start()
  {
    audioSource.clip = bacgroundIntense;
    audioSource.loop = true;
    audioSource.Play();
  }
  public void PlaySFX(AudioClip clip)
  {
    sfxSource.PlayOneShot(clip);
  }
}


