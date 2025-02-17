using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    public static GameManager Instance;

    

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
        }
        
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
