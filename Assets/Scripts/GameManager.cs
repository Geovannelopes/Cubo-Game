using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject hazard2Prefabs; // span da bomba

    [SerializeField]
    private int maxHazard2ToSpan = 5; // span da bomba

    [SerializeField]
    private GameObject hazardPrefabs;

    [SerializeField]
    private int maxHazardToSpan = 3;

    [SerializeField]
    private GameObject mainVCam;

    [SerializeField]
    private GameObject zoomVCam;

    [SerializeField]
    private GameObject gameOverMenu;

    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText;

    [SerializeField]
    private Image backgroundMenu;

    [SerializeField]
    private GameObject player;

    private int highScore;
    private int score;
    private float timer;
    private Coroutine hazardsCoroutine;
    private Coroutine hazardsCoroutine2;

    private bool gameOver;

    private static GameManager instance;
    private const string HighSocrePreferenceKey = "HighScore";

    public static GameManager Instance => instance;
    public int HighScore => highScore;
    void Start()
    {
        instance = this;

        highScore = PlayerPrefs.GetInt(HighSocrePreferenceKey);
        Debug.Log(highScore);
    }
    private void OnEnable()
    {
        player.transform.position = new Vector3(0, 0.75f, 0);
        player.SetActive(true);

        zoomVCam.SetActive(false);
        mainVCam.SetActive(true);

        gameOver = false;
        scoreText.text = "0";
        score = 0;
        timer = 0;

        hazardsCoroutine = StartCoroutine(SpawnHazard());

        hazardsCoroutine2 = StartCoroutine(SpawnHazard2());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                UnPause();
            }
            if (Time.timeScale == 1)
            {
                Pause();
            }
        }

        if (gameOver)
            return;

        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            score++;
            scoreText.text = score.ToString();

            timer = 0;
        }
    }

    private void Pause()
    {
        LeanTween.value(1, 0, 0.75f)
                            .setOnUpdate(SetTimeScale)
                            .setIgnoreTimeScale(true); ;
        backgroundMenu.gameObject.SetActive(true);
    }

    private void UnPause()
    {
        LeanTween.value(0, 1, 0.75f)
            .setOnUpdate(SetTimeScale)
            .setIgnoreTimeScale(true);
        backgroundMenu.gameObject.SetActive(false);
    }

    private void SetTimeScale(float velue)
    {
        Time.timeScale = velue;
        Time.fixedDeltaTime = 0.02f * velue;
    }

    private IEnumerator SpawnHazard()
    { 
        
        var hazardToSpawn = Random.Range(1, maxHazardToSpan);
        if (score < 10)
        {
            for (int i = 0; i < hazardToSpawn; i++)
            {
                var x = Random.Range(-7, 7);
                var drag = Random.Range(0f, 2f);

                var hazard = Instantiate(hazardPrefabs, new Vector3(x, 16, 0), Quaternion.identity);
                hazard.GetComponent<Rigidbody>().drag = drag;
            }
        }
   
        var timeToWait = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(timeToWait);

        yield return SpawnHazard();
    }

    private IEnumerator SpawnHazard2()
    {
       
        var hazard2ToSpawn = Random.Range(1, maxHazard2ToSpan);

        if (score > 10)
        {
            for (int i = 0; i < hazard2ToSpawn; i++)
            {
                var x = Random.Range(-7, 7);
                var drag = Random.Range(0f, 2f);

                var hazard = Instantiate(hazard2Prefabs, new Vector3(x, 16, 0), Quaternion.identity);
                hazard.GetComponent<Rigidbody>().drag = drag;

            }
        }
       

        var timeToWait = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(timeToWait);

        yield return SpawnHazard2();
    }



    public void GameOver()
    {
        StopCoroutine(hazardsCoroutine);
        StopCoroutine(hazardsCoroutine2);
        gameOver = true;

        if (Time.timeScale < 1)
        {
            UnPause();
        }

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(HighSocrePreferenceKey, highScore);
            Debug.Log(highScore);
        }

        mainVCam.SetActive(false);
        zoomVCam.SetActive(true);

        gameObject.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        
        
    }
}
