using DG.Tweening;
using UnityEngine;

public class PlaySoundOnEnable : MonoBehaviour
{
    [SerializeField]
    private MonoEventsPopper eventsPopper;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    [Range(0f, 1f)]
    private float targetVolume = 0.5f;

    private void Awake()
    {
        eventsPopper.OnEnableEvent += OnEnableEvent;
        eventsPopper.OnDisableEvent += OnDisableEvent;
    }

    private void OnEnableEvent()
    {
        if (audioSource != null)
        {
            audioSource.DOFade(targetVolume, 0.1f).From(0).SetLink(gameObject);
            audioSource.Play();
        }
    }

    private void OnDisableEvent()
    {
        if (audioSource.isPlaying)
        {
            audioSource.DOKill();
            audioSource.DOFade(0, 0.2f).SetLink(gameObject);
        }
    }
}
