using System;
using UnityEngine;
using UnityEngine.UI;

public class UnlockableController : MonoBehaviour
{
    [SerializeField]
    private ProgressBar progressBar;
    [SerializeField]
    private ClickerGameManager gameManager;
    [SerializeField]
    private Text progressBarText;
    [SerializeField]
    private Image blockImage;

    [SerializeField]
    private bool unlocked = false;
    [SerializeField]
    private bool visible = true;

    [SerializeField]
    private float progressBarMax = 50;
    [SerializeField]
    private float progressBarUpdateAmount = 1;
    [SerializeField]
    private float progressBarClickAmount = 5;
    [SerializeField]
    private float scoreAmountPerSecond = 10;
    [SerializeField]
    private int unlockPrice = 1;

    private float curentObjectScore = 0;

    void Start()
    {
        progressBar.SetMaximum(progressBarMax);
        UpdateUI();
    }


    void Update()
    {
        progressBar.SetCurrent(curentObjectScore);
        if (curentObjectScore >= progressBarMax)
        {
            gameManager.AddScore(scoreAmountPerSecond);
            curentObjectScore = 0;
        }
    }

    private void UpdateUI()
    {
        if (!visible)
        {
            gameObject.SetActive(false);
        }
        if (visible && !unlocked)
        {
            gameObject.SetActive(true);
            blockImage.gameObject.SetActive(true);
            progressBar.gameObject.SetActive(false);
            progressBarText.gameObject.SetActive(false);
        }
        if (unlocked)
        {
            blockImage.gameObject.SetActive(false);
            progressBar.gameObject.SetActive(true);
            progressBarText.gameObject.SetActive(true);
        }
        progressBarText.text = scoreAmountPerSecond.ToString("F0");
    }

    public void HandleUnlockButtonClick()
    {
        if (gameManager.GetCurrentLevel() >= unlockPrice)
        {
            Unlock();
            gameManager.SetCurrentLevel(gameManager.GetCurrentLevel() - unlockPrice);
        }
    }

    public void HandleButtonClick()
    {
        AddScore(progressBarClickAmount);
    }

    private void AddScore(float scoreAmount)
    {
        curentObjectScore += scoreAmount;
        progressBar.SetCurrent(scoreAmount);
    }

    public void Unlock()
    {
        unlocked = true;
        InvokeRepeating("AddScoreRepeatedly", 0f, 0.05f);
        UpdateUI();
        gameManager.ShowNextUnlockable(this);
    }
    private void AddScoreRepeatedly()
    {
        float addedScore = progressBarUpdateAmount / 2;
        AddScore(addedScore);
    }

    public void SetVisible(bool visible)
    {
        this.visible = visible;
        UpdateUI();
    }

    public bool isLocked()
    {
        return !unlocked;
    }
}
