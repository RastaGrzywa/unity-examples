using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickerGameManager : MonoBehaviour
{
    [SerializeField]
    private ProgressBar mainProgressBar;
    [SerializeField]
    private Text lvlText;

    [SerializeField]
    private float userScore = 0;
    [SerializeField]
    private int currentLvl = 1;
    [SerializeField]
    private float maxLvlScore = 100;

    [SerializeField]
    private List<UnlockableController> unlockables;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        mainProgressBar.SetCurrent(userScore);
        LvlUp();
    }

    private void LvlUp()
    {
        if (userScore >= maxLvlScore)
        {
            currentLvl++;
            userScore = 0;
            maxLvlScore *= 2;
            mainProgressBar.SetMaximum(maxLvlScore);
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        lvlText.text = "LVL: " + currentLvl;
    }

    public void ShowNextUnlockable(UnlockableController unlockable)
    {
        int index = unlockables.IndexOf(unlockable);
        if (index == unlockables.Count - 1)
        {
            return;
        }
        unlockables[index + 1].gameObject.SetActive(true);
        unlockables[index + 1].SetVisible(true);
    }


    public void AddScore(float score)
    {
        userScore += score;
    }

    public int GetCurrentLevel()
    {
        return currentLvl;
    }

    public void SetCurrentLevel(int currentLvl)
    {
        this.currentLvl = currentLvl;
        UpdateUI();
    }

}
