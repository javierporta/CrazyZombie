using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; } = null;

    private int score = 0;

    [SerializeField]
    private Text gameScoreText;


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
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateGameScoreText();
    }

    private void UpdateGameScoreText()
    {
        gameScoreText.text = "Score: " + score.ToString();
    }

    public void AddScore(int scoreToAdd)
    {
        if (scoreToAdd > 0)
        {
            score += scoreToAdd;
            UpdateGameScoreText();
        }
    }

    public void LoadNextLevel()
    {
        //        SceneManager.LoadScene(1);
        StartCoroutine(LoadNextLevelAsync());
    }
    private IEnumerator LoadNextLevelAsync()
    {
        AsyncOperation asyncLoad;
        asyncLoad = SceneManager.LoadSceneAsync(1);

        while (!asyncLoad.isDone)
        {
            print(asyncLoad.progress);
            yield return null;
        }

        print(asyncLoad.progress);
        print("Done!");
        yield return null;

    }


}
