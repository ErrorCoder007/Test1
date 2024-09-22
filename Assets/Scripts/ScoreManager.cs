using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CountdownTimer _countdownTimer;
    [SerializeField] private RecordsData _records;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI _text;

    public static ScoreManager Instance { get; private set; }
    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void AddScore(int point)
    {
        score += point;

        _countdownTimer?.onAddTime.Invoke(2);

        UpdateScoreDisplay();
        SaveRecord();
    }

    public void SubtractFromScore(int point)
    {
        score -= point;
        if (score < 0) score = 0;

        _countdownTimer?.onSubtractTime.Invoke(3);
    }

    private void UpdateScoreDisplay()
    {
        _text.text = score.ToString();
    }

    private void SaveRecord()
    {
        _records.Score = score;
    }
}
