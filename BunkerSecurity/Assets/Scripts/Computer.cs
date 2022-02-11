using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Computer : MonoBehaviour
{
    public TextMeshProUGUI npcNameTxt, npcIDNumberTxt, npcGenderTxt, npcAgeTxt, npcHeightTxt;
    public Image npcPic;
    [SerializeField]
    Sprite noIDPic;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ResetNPCInfo();
    }


    public void SetNPCInfoPic(Sprite p)
    {
        npcPic.sprite = p;
    }

    public void ResetNPCInfo()
    {
        npcNameTxt.text = "";
        npcIDNumberTxt.text = "";
        npcGenderTxt.text = "";
        npcAgeTxt.text = "";
        npcHeightTxt.text = "";
        npcPic.sprite = noIDPic;
    }


}
