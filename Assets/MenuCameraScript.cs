using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCameraScript : MonoBehaviour
{
    public float rotationSpeed;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        transform.Rotate((Vector3.left + (Vector3.up / 2)) * rotationSpeed * Time.deltaTime);
    }
}
