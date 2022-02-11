using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillsCard : MonoBehaviour
{
    float scienceSkill, militarySkill, foodSkill;
    [SerializeField]
    TextMeshProUGUI nameTxt, idNumberTxt;
    [SerializeField]
    Image idPic;
    [SerializeField]
    Transform scienceSkillT, militarySkillT, foodSkillT;
    [SerializeField]
    float maxSkillScale = 7;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetIDPic(Sprite s)
    {
        idPic.sprite = s;
    }

    public void SetName(string t)
    {
        nameTxt.text = t;
    }

    public void SetIDNumber(string t)
    {
        idNumberTxt.text = t;
    }

    public void SetScienceSkill(float v)
    {
        v = Mathf.Clamp01(v);
        scienceSkill = v;
        scienceSkillT.localScale = new Vector3(v * maxSkillScale, 1, 1);
    }

    public void SetMilitarySkill(float v)
    {
        v = Mathf.Clamp01(v);
        militarySkill = v;
        militarySkillT.localScale = new Vector3(v * maxSkillScale, 1, 1);
    }

    public void SetFoodSkill(float v)
    {
        v = Mathf.Clamp01(v);
        foodSkill = v;
        foodSkillT.localScale = new Vector3(v * maxSkillScale, 1, 1);
    }

    public float GetScienceSkill()
    {
        return scienceSkill;
    }

    public float GetMilitarySkill()
    {
        return militarySkill;
    }

    public float GetFoodSkill()
    {
        return foodSkill;
    }
}
