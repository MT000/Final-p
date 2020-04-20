
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText;
    public Text timerText;
    public AudioSource musicSource;
    public AudioClip musicClipOne;

    private bool gameOver;
    private bool restart;
    public int score;

   

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();

        StartCoroutine(SpawnWaves());
        StartCoroutine(reloadTimer(30));

    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                SceneManager.LoadScene("SampleScene");
            }
            
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    IEnumerator reloadTimer(float reloadTimeInSeconds)
    {
        float counter = 0;

        while (counter < reloadTimeInSeconds)
        {
            counter += Time.deltaTime;
            timerText.text = counter.ToString();
            yield return null;
        }

    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'J' for Restart";
                restart = true;
                break;

            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {

            Destroy(collision.collider.gameObject);
        }

    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;

        if (score >= 100)
        {
            winText.text = "You win!GAME CREATED BY Tong Mu";
            gameOver = true;
            restart = true;
            musicSource.clip = musicClipOne;
            musicSource.Play();

        }

    }
    public void Gameover()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }
}