using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGameButton : MonoBehaviour
{
    public GameObject hint;
    public float endTime;
    private bool ending = false;
    private bool used = false;
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !used)
        {
            hint.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            hint.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Action") && hint.activeSelf)
        {
            audio.Play();
            hint.SetActive(false);
            used = true;
            ending = true;
            FindAnyObjectByType<MainGameManager>().UpdateLabel("You won!\nThanks for playing");
        }
        if (ending)
        {
            endTime -= Time.deltaTime;
        }
        if (endTime < 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(0);
        }
    }
}
