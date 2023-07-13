using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    private bool pause = false;
    public TextMeshProUGUI health;
    public TextMeshProUGUI label;
    public CombatScript player;
    public GameObject pauseMenu;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateLabel(string text)
    {
        label.text = text;
    }

    public void UpdateUI()
    {
        health.text = "Lifes: " + player.health;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!pause)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pauseMenu.SetActive(true);
            } 
            else
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                pauseMenu.SetActive(false);
            }
            pause = !pause;
        }
    }
}
