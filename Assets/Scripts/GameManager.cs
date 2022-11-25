using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool isGameStart = false;
    [HideInInspector] public bool isGoingAway;

    [SerializeField] private GameObject airCraft;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject warningText;
    public Slider gear;
    
    public void StartGame()
    {
        inGamePanel.SetActive(true);
        startPanel.SetActive(false);
        isGameStart = true;
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameOver()
    {
        Invoke("SetActiveGameOverPanel",1.5f);
    }
    private void SetActiveGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
    public void Warning()
    {
        isGoingAway = true;
        warningText.SetActive(true);
        StartCoroutine(CountDown());
    }
    public void ExitCeiling()
    {
        isGoingAway = false;
        warningText.SetActive(false);
    }
    IEnumerator  CountDown()
    {
        yield return new WaitForSeconds(3);
        if (isGoingAway)
        {
            gameOverPanel.SetActive(true);
            airCraft.SetActive(false);
        }
    }
    
}
