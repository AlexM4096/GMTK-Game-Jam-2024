using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    
    private TMP_Text _timerText;
    enum TimerType {Countdown, Stopwatch}

    [SerializeField] private bool _isRunning = false;

    [SerializeField] private TimerType _timerType;
    [SerializeField] private float _time = 60.0f;

    public event Action Finished;

    public void StartTimer() => _isRunning = true;
    public void StopTimer() => _isRunning = false;

    private void Awake()
    {
        _timerText = GetComponent<TMP_Text>();
    }

    private void EndTimer() 
    {
        StopTimer();
        Finished?.Invoke();
        Debug.Log("Finished");
    }


    private void UpdateTimer(float time) => _time = time;

    private void Update()
    {
        if (!_isRunning) return;
        if (_timerType == TimerType.Countdown && _time < 0f)
        {
            EndTimer();
            UpdateTimer(0.0f);
            return;
        }
        
        _time += _timerType == TimerType.Countdown ? -Time.deltaTime : Time.deltaTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(_time);

        _timerText.text = timeSpan.ToString(@"mm\:ss\:ff");
    }


}
