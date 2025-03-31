using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

}
