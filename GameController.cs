using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public UnityEngine.UI.Text scoreLabel;
    public UnityEngine.UI.Text maxScoreLabel;
    public GameObject menu;
    public UnityEngine.UI.Button startButton;
    public UnityEngine.UI.Button exitButton;
    public UnityEngine.UI.Toggle musicToggle;

    public bool isGameStarted = false;
    public static GameController instance;

    int score = 0;
    int maxScore = 0;
    GameObject playerShip;
    Vector3 playerPosition;

    /// <summary>
    /// Изменение поля с очками
    /// </summary>
    /// <param name="score">количество очков</param>
    public void scoreLabelChange(int score)
    {
        scoreLabel.text = "score: " + score;
    }

    /// <summary>
    /// Увеличение количества очков
    /// </summary>
    /// <param name="increment">на сколько увеличить сумму очков</param>
    public void incrementScore(int increment)
    {
        score += increment;
        scoreLabelChange(score);

        if (score > maxScore)
        {
            maxScore = score;
            maxScoreLabel.text = "maximum score: " + maxScore;
        }
    }

    /// <summary>
    /// Изменение состояния меню
    /// </summary>
    /// <param name="isGameStarted">Состояние игры</param>
    void menuChangeVisible(bool isGameStarted)
    {
        if (isGameStarted)
        {
            menu.gameObject.SetActive(false);

            scoreLabel.gameObject.SetActive(true);

            if (!playerShip.activeSelf) {
                playerShip.transform.position = playerPosition;
                playerShip.SetActive(true);
            }
        }
        else
        {
            menu.gameObject.SetActive(true);
            scoreLabel.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        instance = this;
        menuChangeVisible(isGameStarted);
        playerShip = GameObject.FindGameObjectWithTag("Player");
        playerPosition = playerShip.transform.position;

        // Начать игру
        startButton.onClick.AddListener(delegate {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
                Destroy(enemy);

            score = 0;
            scoreLabelChange(score);

            isGameStarted = true;
        });

        // Закончить игру
        exitButton.onClick.AddListener(delegate {
            Application.Quit();
        });

        // Вкл\выкл музыки
        musicToggle.onValueChanged.AddListener(delegate {
            if(musicToggle.isOn)
            {
                GetComponent<AudioSource>().Play();
            }
            else
            {
                GetComponent<AudioSource>().Stop();
            }
        });
    }

    private void Update()
    {
        menuChangeVisible(isGameStarted);
    }
}
