using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{

    public PLayer player;
    public Text coinText;
    public Image[] hearts;
    public Sprite isLife, nonLife;
    public GameObject PauseScreen;
    public GameObject winScreen;
    public GameObject gameOverScreen;
    float timer = 0f;
    public Text timeText;

    //Создали метод который перезапускает текущую сцену
    public void Reload()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Update()
    {
        coinText.text = player.GetCoins().ToString();

        for (int i = 0; i < hearts.Length; i++)
        {
            if (player.GetHP() > i)
                hearts[i].sprite = isLife;
            else
                hearts[i].sprite = nonLife;
        }

        timer += Time.deltaTime;
        timeText.text = timer.ToString("F2").Replace(",", ":");
    }

    public void PauseOn()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        PauseScreen.SetActive(true);

    }

    public void PauseOff()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        PauseScreen.SetActive(false);
    }

    public void Win()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        winScreen.SetActive(true);

        if (!PlayerPrefs.HasKey("LevelComplete") || PlayerPrefs.GetInt("LevelComplete") < SceneManager.GetActiveScene().buildIndex)
            PlayerPrefs.SetInt("LevelComplete", SceneManager.GetActiveScene().buildIndex);
        print(PlayerPrefs.GetInt("LevelComplete"));
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        gameOverScreen.SetActive(true);
    }

    public void openMenuPanel()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene("Menu");
    }

    public void NextLVL()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}