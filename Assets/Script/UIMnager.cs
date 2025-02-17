using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMnager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject retayBtn;
    public GameObject lobbyBtn;
    public GameObject startBtn;

    public static UIMnager Instance;

    UIMnager uiMnager;

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
    // Start is called before the first frame update
    void Start()
    {
        retayBtn.SetActive(false);
        lobbyBtn.SetActive(false);
    }

    public void SetRestart()
    {
        Time.timeScale = 0f;
        retayBtn.SetActive(true);
        lobbyBtn.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

}
