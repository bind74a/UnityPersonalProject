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
    }

    void Start()
    {
        
    }

    public void GameOver()
    {

    }

    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;//���� ���� �̸��� ������ �ִ´�

        switch (currentSceneName)//������ ���� ���� ����ŸƮ ��ư ��ȭ
        {
            case "Game1Scene":
                SceneManager.LoadScene("Game1Scene");
                break;
        }
        
    }

    public void RetunLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void PlaneAddScore(int score)
    {
        planeGameScore += score;
    }
}
