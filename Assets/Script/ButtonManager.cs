using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void RetunLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }


    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;//���� ���� �̸��� ������ �ִ´�

        switch (currentSceneName)//������ ���� ���� ����ŸƮ ��ư ��ȭ
        {
            case "Game1Scene":
                SceneManager.LoadScene("Game1Scene");
                break;
            case "Game2Scene":
                SceneManager.LoadScene("Game2Scene");
                break;
        }

    }

    public void StartGame()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

}
