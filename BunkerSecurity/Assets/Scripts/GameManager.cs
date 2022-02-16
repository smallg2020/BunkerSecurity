using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float timePerSecond = 10;
    public float[] currentTime = new float[3];
    public float[] deskJobStartTime = new float[3];
    public int idFlawChance = 10;
    public int idFlaws = 2;
    public int skillsFlawChance = 10;
    public int skillsFlaws = 0;
    public int currentScience, currentMilitary, currentFoodProduction;


    public List<Clock> clocks = new List<Clock>();

    // Start is called before the first frame update
    void Start()
    {
        SetTime(deskJobStartTime);
    }

    // Update is called once per frame
    void Update()
    {
        float ft = Time.deltaTime;
        currentTime[2] += ft * timePerSecond;
        //print(currentTime[2]);
        if (currentTime[2] >= 60)
        {
            currentTime[2] = ft * timePerSecond;
            currentTime[1]++;
            if (currentTime[1] >= 60)
            {
                currentTime[1] = 0;
                currentTime[0]++;
                if (currentTime[0] >= 12)
                {
                    currentTime[0] = 0;
                }
            }
        }
        //print(currentTime[0] + ":" + currentTime[1] + ":" + currentTime[2]);
        foreach (Clock c in clocks)
        {
            c.UpdateTime(currentTime);
        }
    }

    public void SetTime(float[] t)
    {
        currentTime[0] = t[0];
        currentTime[1] = t[1];
        currentTime[2] = t[2];
    }
}
