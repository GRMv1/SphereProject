using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Button;

    private bool isPause = true;
    public bool IsPause
    {
        get
        {
            return isPause;
        }

        set
        {
            isPause = value;
        }
    }

    private bool isGameStarted = false;
    public bool IsGameStarted
    {
        get
        {
            return isGameStarted;
        }

        set
        {
            isGameStarted = value;
        }
    }

    private void Start()
    {
        Time.timeScale = 0f;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGameStarted)
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        isPause = true;
        StartCoroutine(WaitFor5Seconds());
        Time.timeScale = 0f;
        
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        Button.SetActive(false);
        isGameStarted = true;
        isPause = false;
    }

    IEnumerator WaitFor5Seconds()
    {
        yield return new WaitForSecondsRealtime(5);
        ResumeGame();
        isPause = false;
    }

    
}
