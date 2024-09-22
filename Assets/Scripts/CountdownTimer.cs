using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private int startTimeInSeconds = 20;

    // events
    public UnityEvent<int> onAddTime;
    public UnityEvent<int> onSubtractTime;
    public UnityEvent onTimerEnded;

    private float remainingTime;

    private void OnEnable()
    {
        onAddTime.AddListener(AddTimeToTimer);
        onSubtractTime.AddListener(SubtractTimeFromTimer);

        remainingTime = startTimeInSeconds;
        StartCoroutine(UpdateTimer());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void AddTimeToTimer(int value)
    {
        if (value > 0) remainingTime += value;
    }

    public void SubtractTimeFromTimer(int value)
    {
        if (value > 0) remainingTime -= value;
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingTime > 0)
        {
            remainingTime -= 1f;
            timeText.text = remainingTime.ToString("0") + "с";

            yield return new WaitForSeconds(1f);
        }

        onTimerEnded.Invoke();
    }
}
