using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [SerializeField] GameObject gameOverPanel;

    private void Start()
    {
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (SnakeController.isDead)
        {
            GameOver();
        }
    }

    public void Restart()
    {
        Debug.Log("Restart!");
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Restart!!");
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

}
