using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public enum UIState
{
    Home,
    Game,
    GameOver

}
public class UIMnager : MonoBehaviour
{
    HomeUI homeUI;
    GameUI gameUI;
    GameOverUI gameOverUI;

    private UIState currentstate;

    //plane 게임 버튼 과 ui
    public TextMeshProUGUI planeScoreText;
    public TextMeshProUGUI planeBestScoreText;
    public GameObject retayBtn;
    public GameObject lobbyBtn;
    public GameObject startBtn;

    public static UIMnager Instance;

    UIMnager uiMnager;

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
        currentSceneName = SceneManager.GetActiveScene().name;
       
        homeUI = GetComponent<HomeUI>();
        homeUI.Init(this);
        gameUI = GetComponent<GameUI>();
        gameUI.Init(this);
        gameOverUI = GetComponent<GameOverUI>();
        gameOverUI.Init(this);

        ChangeState(UIState.Home);
    }
    // Start is called before the first frame update
    void Start()
    {
        retayBtn.SetActive(false);
        lobbyBtn.SetActive(false);
    }

    public void SetPlayGame()
    {
        ChangeState(UIState.Game);
    }

    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
    }

    public void ChangeState(UIState state)
    {
        currentstate = state;
        homeUI.SetActive(currentstate);
        gameUI.SetActive(currentstate);
        gameOverUI.SetActive(currentstate);
    }

    public void ChangeWave(int waveIndex)
    {
        gameUI.UpdateWaveText(waveIndex);
    }

    public void ChangePlayerHP(float currentHP, float maxHP)
    {
        gameUI.UpdateHPSlider(currentHP / maxHP);
    }

    public void SetRestart()
    {
        Time.timeScale = 0f;
        retayBtn.SetActive(true);
        lobbyBtn.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        planeScoreText.text = score.ToString();
    }
    public void BestScoreUpdate(int bestscore)
    {
        planeBestScoreText.text = bestscore.ToString();
    }


}
