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
        string currentSceneName = SceneManager.GetActiveScene().name;//현재 씬의 이름을 변수에 넣는다

        switch (currentSceneName)//현재의 씬에 따라 리스타트 버튼 변화
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
