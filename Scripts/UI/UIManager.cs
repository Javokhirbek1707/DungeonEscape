using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UI_Manager is NULL!");
            }

            return _instance;
        }
    }

    public Text playerGemsCountText;
    public Image selectionImage;
    public Text gemCountText;
    public Image[] HealthBars;

    private void Awake()
    {
        _instance = this;
    }

    public void ShopOpen(int gemsCount)
    {
        playerGemsCountText.text = "" + gemsCount + "G";
    }

    public void UpdateShopSelection(int yPos)
    {
        selectionImage.rectTransform.anchoredPosition = new Vector2(selectionImage.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateGemCount(int count)
    {
        gemCountText.text = "" + count;
    }

    public void UpdateLives(int livesRemain)
    {
        for(int i = 0; i <= livesRemain; i++)
        {
            if (i == livesRemain)
            {
                HealthBars[i].enabled = false;
            }

        }
    }
}
