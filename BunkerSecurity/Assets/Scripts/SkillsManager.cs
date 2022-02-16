using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    [SerializeField]
    Transform scienceBar, militaryBar, foodBar;
    [SerializeField]
    int maxScience = 100, maxMilitary = 100, maxFoodProduction = 100;
    [SerializeField]
    int currentScience, currentMilitary, currentFoodProduction;

    public void SetCurrentScience(int v)
    {
        v = Mathf.Clamp(v, 0, maxScience);
        currentScience = v;
        UpdateScienceUI();
    }

    public void SetCurrentMilitary(int v)
    {
        v = Mathf.Clamp(v, 0, currentMilitary);
        currentMilitary = v;
        UpdateMilitaryUI();
    }

    public void SetCurrentFoodProduction(int v)
    {
        v = Mathf.Clamp(v, 0, currentFoodProduction);
        currentFoodProduction = v;
        UpdateFoodUI();
    }

    public void UpdateCurrentScience(int v)
    {
        currentScience += v;
        currentScience = Mathf.Clamp(currentScience, 0, maxScience);
        UpdateScienceUI();
    }

    public void UpdateCurrentMilitary(int v)
    {
        currentMilitary += v;
        currentMilitary = Mathf.Clamp(currentMilitary, 0, maxMilitary);
        UpdateMilitaryUI();
    }

    public void UpdateCurrentFoodProduction(int v)
    {
        currentFoodProduction += v;
        currentFoodProduction = Mathf.Clamp(currentFoodProduction, 0, maxFoodProduction);
        UpdateFoodUI();
    }

    public int GetCurrentScience()
    {
        return currentScience;
    }

    public int GetCurrentMilitary()
    {
        return currentMilitary;
    }

    public int GetCurrentFoodProduction()
    {
        return currentFoodProduction;
    }

    void UpdateScienceUI()
    {
        scienceBar.transform.localScale = new Vector3(currentScience / 100.0f * 7, 1, 1);
    }

    void UpdateMilitaryUI()
    {
        militaryBar.transform.localScale = new Vector3(currentMilitary / 100.0f * 7, 1, 1);
    }

    void UpdateFoodUI()
    {
        foodBar.transform.localScale = new Vector3(currentFoodProduction / 100.0f * 7, 1, 1);
    }

}
