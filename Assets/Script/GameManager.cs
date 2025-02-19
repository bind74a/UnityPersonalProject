using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    public static GameManager Instance;

    //대화창
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;

    private int planeGameScore = 0;

    private string currentSceneName;

    UIMnager uiMnager;
    public UIMnager uIMnager { get{ return uiMnager; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }    

        uiMnager = FindObjectOfType<UIMnager>();
    }

    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        switch(currentSceneName)
        {
            case "LobbyScene":
                Time.timeScale = 1f;
                break;
            case "Game1Scene":
                Time.timeScale = 0f;
                uiMnager.UpdateScore(0);
                break;
            case "Game2Scene":
                Time.timeScale = 1f;
                break;
        }
        
    }
    /// <summary>
    /// 대화 액션
    /// </summary>
    /// <param name="scanObj"></param>
    public void LobbyAction(GameObject scanObj)
    {
        if (isAction)
        {
            isAction = false;
        }
        else
        {
            isAction = true;
            scanObject = scanObj;
            talkText.text = "test 대화액션";
        }
        talkPanel.SetActive(isAction);
    }

    public void GameOver()
    {
        uiMnager.SetRestart();
    }

    public void PlaneAddScore(int score)
    {
        planeGameScore += score;
        uiMnager.UpdateScore(planeGameScore);
    }
}
