using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillsCard : MonoBehaviour
{
    int scienceSkill, militarySkill, foodSkill;
    [SerializeField]
    TextMeshProUGUI nameTxt, idNumberTxt;
    [SerializeField]
    Image idPic;
    [SerializeField]
    Transform scienceSkillT, militarySkillT, foodSkillT;
    [SerializeField]
    float maxSkillScale = 7;

    [SerializeField]
    int skillFlaws = 0;


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

    public void SetScienceSkill(int v)
    {
        v = Mathf.Clamp(v, 0, 10);
        scienceSkill = v;
        scienceSkillT.localScale = new Vector3(v * maxSkillScale * 0.1f, 1, 1);
    }

    public void SetMilitarySkill(int v)
    {
        v = Mathf.Clamp(v, 0, 10);
        militarySkill = v;
        militarySkillT.localScale = new Vector3(v * maxSkillScale * 0.1f, 1, 1);
    }

    public void SetFoodSkill(int v)
    {
        v = Mathf.Clamp(v, 0, 10);
        foodSkill = v;
        foodSkillT.localScale = new Vector3(v * maxSkillScale * 0.1f, 1, 1);
    }

    public void ChangePic(Sprite s)
    {
        idPic.sprite = s;
    }

    public Sprite GetIDPicture()
    {
        return idPic.sprite;
    }

    public void ChangeName(string n)
    {
        nameTxt.text = n;
    }

    public string GetName()
    {
        return nameTxt.text;
    }

    public void ChangeIDNumber(string s)
    {
        idNumberTxt.text = s;
    }

    public string GetIDNumber()
    {
        return idNumberTxt.text;
    }

    public void UpdateSkillFlaws(int v)
    {
        skillFlaws += v;
    }

    public int GetSkillFlaws()
    {
        return skillFlaws;
    }

    public int GetScienceSkill()
    {
        return scienceSkill;
    }

    public int GetMilitarySkill()
    {
        return militarySkill;
    }

    public int GetFoodSkill()
    {
        return foodSkill;
    }
}
