using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesManager : MonoBehaviour
{

    public static EnemiesManager Instance { get; private set; } = null;


    [SerializeField]
    private Text enemiesText;

    private int enemiesCount = 0;

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

    private void Start()
    {
        UpdateEnemiesText();
    }

    private void UpdateEnemiesText()
    {
        enemiesText.text = "Enemies: " + enemiesCount.ToString();
    }

    public void AddEnemy()
    {
        enemiesCount++;
        UpdateEnemiesText();
        
    }

    public void RemoveEnemy()
    {
        enemiesCount--;
        UpdateEnemiesText();
    }
}
