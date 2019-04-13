using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStatus : MonoBehaviour
{
    //config
    [Range(0f, 1f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int scorePerBlock = 50;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoPlayEnabled;

    //State Variables
    [SerializeField] int playerScore = 0;

    private void Awake()
    {
        int gameStatusObjectCount = FindObjectsOfType<GameStatus>().Length;

        if (gameStatusObjectCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        scoreText.text = playerScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void IncreasePlayerScore()
    {
        playerScore += scorePerBlock;
        scoreText.text = playerScore.ToString();
    }

    public void destroyGameStatus()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}
