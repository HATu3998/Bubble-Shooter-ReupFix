using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
  public  Text GameStatus;
    public Text ScoreText;
    int score;
    bool isGameOver;
    public static GameController controller;
    private void Awake()
    {
        controller = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGameOver)
        {
            Time.timeScale = 1f;
            isGameOver = false;
            SceneManager.LoadScene("SampleScene");
        }
    }
    public void GetScoreText()
    {
        ScoreText.text = "Score :" + ++score;
    }
    public void SetGameOver()
    {
       Time.timeScale = 0;
        isGameOver = true;
        GameStatus.gameObject.SetActive(true);
    }
}
