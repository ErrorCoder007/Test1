using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;

    [Space(10)]
    [Header("Button")]
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _playButton;
    [SerializeField] private GameObject _menuButton;

    public void ClickButtonPause()
    {
        SetActivePauseMenu(true);
        Pause();
    }

    private void Pause()
    {
        Time.timeScale = 0.0f;
    }

    public void ClickButtonPlay()
    {
        SetActivePauseMenu(false);
        Play();
    }

    private void Play()
    {
        Time.timeScale = 1.0f;
    }

    public void ClickButtonMenu()
    {
        SetActivePauseMenu(false);
        _menu.GetComponent<Menu>().ActiveMenu(true);

        Time.timeScale = 1.0f;

        gameObject.SetActive(false);
    }

    private void SetActivePauseMenu(bool active)
    {
        _playButton.SetActive(active);
        _pauseButton.SetActive(!active);
        _menuButton.SetActive(active);
    }
}
