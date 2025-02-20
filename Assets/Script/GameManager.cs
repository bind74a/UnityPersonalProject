using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    public static GameManager Instance;
    //ž�� �̴ϰ��� �÷��̾�
    public PlayerController topDownPlayer;
    private ResourcController tdPlayerResourceController;

    [SerializeField] private int currentWaveIndex = 0;

    private EnemyManager enemyManager;


    //��ȭâ
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;


    //����� ���� �ý���
    private int planeGameScore = 0;

    int planeGameBestScore = 0;
    private int PlaneGameBestScore { get => planeGameBestScore; }

    private string currentSceneName;

    private const string PlaneScorekey = "PlaneBestScore";
    
    
    //ui�Ŵ��� ����
    UIMnager uiMnager;
    public static bool isFirstLoading = true;
    public UIMnager uIMnager { get{ return uiMnager; } }

    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        uiMnager = FindObjectOfType<UIMnager>();

        planeGameBestScore = PlayerPrefs.GetInt(PlaneScorekey, 0);
        

        switch (currentSceneName)
        {
            case "Game2Scene":
                //ž�ٿ� �̴ϰ��� ����
                topDownPlayer = FindObjectOfType<PlayerController>();
                topDownPlayer.Init(this);

                enemyManager = GetComponentInChildren<EnemyManager>();
                enemyManager.Init(this);

                tdPlayerResourceController = topDownPlayer.GetComponent<ResourcController>();
                tdPlayerResourceController.RemveHealthChangeEvent(uiMnager.ChangePlayerHP);
                tdPlayerResourceController.AddHealthChangeEvent(uiMnager.ChangePlayerHP);
                break;

        }
    }

    void Start()
    {
        
        switch(currentSceneName)
        {
            case "LobbyScene":
                Time.timeScale = 1f;
                break;
            case "Game1Scene":
                Time.timeScale = 0f;
                uiMnager.UpdateScore(0);
                uiMnager.BestScoreUpdate(planeGameBestScore);
                break;
            case "Game2Scene":
                Time.timeScale = 1f;
                if (!isFirstLoading)
                {
                    StartGame();
                }
                else
                {
                    isFirstLoading = false;
                }
                break;
        }
        
        

    }
    #region //ž�ٿ� �̴ϰ��� �ɼ�
    public void StartGame()
    {
        if (currentSceneName == "Game2Scene")
        {
            uiMnager.SetPlayGame();
            StartNextWave();
        }
        
    }

    void StartNextWave()
    {
        if (currentSceneName == "Game2Scene")
        {
            currentWaveIndex += 1;
            enemyManager.StartWave(1 + currentWaveIndex / 5);

            uiMnager.ChangeWave(currentWaveIndex);
        }
    }

    public void EndOfWave()
    {
        if (currentSceneName == "Game2Scene")
        {
            StartNextWave();
        }
    }

    #endregion

    private void Update()
    {

        
    }

    /// <summary>
    /// ��ȭ �׼�
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
            talkText.text = "test ��ȭ�׼�";
        }
        talkPanel.SetActive(isAction);
    }
    public void GameOver()
    {
        switch(currentSceneName)
        {
            case "Game1Scene":
                uiMnager.SetRestart();
                break;
            case "Game2Scene":
                enemyManager.StopWave();
                uiMnager.SetGameOver();
                break;
        }
        
    }

    public void PlaneAddScore(int score)
    {
        planeGameScore += score;
        uiMnager.UpdateScore(planeGameScore);
    }

    public void BestUpdateScore()
    {
        if(planeGameScore > planeGameBestScore)
        {
            planeGameBestScore = planeGameScore;

            PlayerPrefs.SetInt(PlaneScorekey, planeGameBestScore);
        }
    }
}
