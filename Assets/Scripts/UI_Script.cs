using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    [SerializeField] private Text TimeRemainsText;
    [SerializeField] private Text killsCountText;
    [SerializeField] private Text bulletsCountText;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject ResultPanel;
    [SerializeField] private SaveScript saveScript;
    [SerializeField] private Text loadAmmoText;
    [SerializeField] private Text AllAmmoText;
    [SerializeField] private Text Health;
    [SerializeField] private Text Kills;
    [SerializeField] private Text TimeTetx;
    private float timeElapsed = 0;
    private void Start()
    {
        StartCoroutine(CountdownTimer());
    }
    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        Player player = GameObject.Find("Player").GetComponent<Player>();
        Health.text = "HP: " + Player.health;
        AllAmmoText.text = PlayerPrefs.GetInt("AllAmmo").ToString();
        loadAmmoText.text = PlayerPrefs.GetInt("loadAmmo").ToString();
        killsCountText.text = "Вбито ворогів: " + PlayerPrefs.GetInt("Kills");
        Kills.text = "Вбито ворогів: " + PlayerPrefs.GetInt("Kills");
    }
    private IEnumerator CountdownTimer()
    {

        while (true)
        {
            TimeSpan time = TimeSpan.FromSeconds(timeElapsed);
            TimeRemainsText.text = "Час: " + time.ToString(@"mm\:ss");
            TimeTetx.text = "Ваш час: " + time.ToString(@"mm\:ss");

            yield return new WaitForSeconds(1f);

            timeElapsed++;
        }

    }
    public void LoseGame()
    {
        SceneManager.LoadScene(0);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pausePanel.SetActive(true);
        gamePanel.SetActive(false);
    }
    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pausePanel.SetActive(false);
        gamePanel.SetActive(true);
        Time.timeScale = 1;
    }
    public void SaveGame()
    {
        ResumeGame();
        saveScript.SaveGame();
    }
    public void LoadGame()
    {
        saveScript.LoadGame();
        ResumeGame();
    }
    public void ExitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void EndGame()
    {
        ResultPanel.SetActive(true);
        Time.timeScale = 0;
        gamePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(1);
    }

}
