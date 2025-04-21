using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is Null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public TMP_Text playerGemCountText;
    public Image selectionImage;
    public TMP_Text gemCountText;
    public Image[] healthBars;
    public TMP_Text countDownText;
    public bool gameOver = false;

    public void OpenShop( int gemCount)
    {
        playerGemCountText.text = $"{gemCount}G";
    }

    public void UpdateShopSelection( int yPos)
    {
        selectionImage.rectTransform.anchoredPosition = new Vector2(selectionImage.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateGemCount(int count)
    {
        gemCountText.text = $"{count}";
    }

    public void UpdateLives (int livesRemaining)
    {
        for (int i = 0; i <= livesRemaining; i++)
        {
            if (i == livesRemaining)
            {
                healthBars[i].enabled = false;
            }
        }
    }

    IEnumerator CountDown(int time)
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1f);
            time--;
            countDownText.text = $"TIME LEFT : {time}";
        }
        gameOver = true;
    }

    public void StartTimer(int time)
    {
        StartCoroutine(CountDown(time));
    }

    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
