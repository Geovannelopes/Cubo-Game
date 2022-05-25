using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject hazard5Prefabs; // span do monstro

    [SerializeField]
    private int maxHazard5ToSpan = 7; // span do monstro

    [SerializeField]
    private GameObject hazard4Prefabs; // span da abelha

    [SerializeField]
    private int maxHazard4ToSpan = 6; // span da abelha

    [SerializeField]
    private GameObject hazard3Prefabs; // spawn do esqueleto

    [SerializeField]
    private int maxHazard3ToSpan = 5; // spawn do esqueleto

    [SerializeField]
    private GameObject hazard2Prefabs; // spawn da bomba

    [SerializeField]
    private int maxHazard2ToSpan = 4; // spawn da bomba

    [SerializeField]
    private GameObject hazardPrefabs; // spawn da caixa

    [SerializeField]
    private int maxHazardToSpan = 3; // spawn da caixa

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
    private Coroutine hazardsCoroutine3;
    private Coroutine hazardsCoroutine4;
    private Coroutine hazardsCoroutine5;

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

        hazardsCoroutine3 = StartCoroutine(SpawnHazard3());

        hazardsCoroutine4 = StartCoroutine(SpawnHazard4());

        hazardsCoroutine5 = StartCoroutine(SpawnHazard5());
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
                var drag = Random.Range(0f, 1f);

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

        if (score > 10 && score < 20)
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
    
    private IEnumerator SpawnHazard3()
    {

        var hazard3ToSpawn = Random.Range(1, maxHazard3ToSpan);

        if (score > 20 && score < 30)
        {
            for (int i = 0; i < hazard3ToSpawn; i++)
            {
                var x = Random.Range(-7, 7);
                var drag = Random.Range(0f, 2f);

                var hazard = Instantiate(hazard3Prefabs, new Vector3(x, 16, 0), Quaternion.identity);
                hazard.GetComponent<Rigidbody>().drag = drag;

            }
        }


        var timeToWait = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(timeToWait);

        yield return SpawnHazard3();
    }

    private IEnumerator SpawnHazard4()
    {

        var hazard4ToSpawn = Random.Range(1, maxHazard4ToSpan);

        if (score > 30 && score < 40)
        {
            for (int i = 0; i < hazard4ToSpawn; i++)
            {
                var x = Random.Range(-7, 7);
                var drag = Random.Range(0f, 2f);

                var hazard = Instantiate(hazard4Prefabs, new Vector3(x, 16, 0), Quaternion.identity);
                hazard.GetComponent<Rigidbody>().drag = drag;

            }
        }


        var timeToWait = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(timeToWait);

        yield return SpawnHazard4();
    }

    private IEnumerator SpawnHazard5()
    {

        var hazard5ToSpawn = Random.Range(1, maxHazard5ToSpan);

        if (score > 40 && score < 50)
        {
            for (int i = 0; i < hazard5ToSpawn; i++)
            {
                var x = Random.Range(-7, 7);
                var drag = Random.Range(0f, 2f);

                var hazard = Instantiate(hazard5Prefabs, new Vector3(x, 16, 0), Quaternion.identity);
                hazard.GetComponent<Rigidbody>().drag = drag;

            }
        }


        var timeToWait = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(timeToWait);

        yield return SpawnHazard5();
    }

    public void GameOver()
    {
        StopCoroutine(hazardsCoroutine);
        StopCoroutine(hazardsCoroutine2);
        StopCoroutine(hazardsCoroutine3);
        StopCoroutine(hazardsCoroutine4);
        StopCoroutine(hazardsCoroutine5);

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
