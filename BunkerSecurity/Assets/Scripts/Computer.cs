using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Computer : MonoBehaviour
{
    public TextMeshProUGUI npcNameTxt, npcIDNumberTxt, npcGenderTxt, npcAgeTxt, npcHeightTxt;
    [SerializeField]
    GameObject[] pages;

    public GameObject showingInfoForNPC;

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
        showingInfoForNPC = null;
        npcNameTxt.text = "";
        npcIDNumberTxt.text = "";
        npcGenderTxt.text = "";
        npcAgeTxt.text = "";
        npcHeightTxt.text = "";
        npcPic.sprite = noIDPic;
    }

    public void OpenPage(GameObject pageToOpen)
    {
        foreach (GameObject page in pages)
        {
            page.SetActive(page == pageToOpen);
        }
    }


}
