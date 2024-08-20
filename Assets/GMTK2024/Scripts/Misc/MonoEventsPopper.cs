using UnityEngine;

public class MonoEventsPopper : MonoBehaviour
{
    public event System.Action OnEnableEvent;
    public event System.Action OnDisableEvent;

    private void OnEnable()
    {
        OnEnableEvent?.Invoke();
    }

    private void OnDisable()
    {
        OnDisableEvent?.Invoke();
    }
}
