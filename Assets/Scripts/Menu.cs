using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CountdownTimer _countdownTimer;
    [SerializeField] private RecordsData _records;
    [SerializeField] private ItemSpawner _itemSpawner;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI _highScoreDisplay;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _scoreDisplay;

    private void Start()
    {
        _countdownTimer.onTimerEnded.AddListener(StopGame);
    }

    private void StopGame()
    {
        ActiveMenu(true);
    }

    public void ClickButtonStart()
    {
        StartGame();
    }

    public void ActiveMenu(bool active)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }

        _pauseMenu.SetActive(!active);
        _scoreDisplay.SetActive(!active);
        _countdownTimer.gameObject.SetActive(!active);

        UpdateRecardScore();
    }

    private void StartGame()
    {
        _itemSpawner.RestartSpawn();
        ActiveMenu(false);
    }

    private void UpdateRecardScore()
    {
        _highScoreDisplay.text = _records.Score.ToString();
    }
}
