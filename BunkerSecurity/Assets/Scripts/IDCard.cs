using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class IDCard : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI nameTxt, idNumberTxt, genderTxt, ageTxt, heightTxt, expiryTxt;

    [SerializeField]
    Image idPic;

    NPCManager npcManager;
    DeskJobManager deskJobM;

    public bool playerIsHolding = false;
    public DeskJobManager.ApprovalStatus approvalStatus = DeskJobManager.ApprovalStatus.None;
    int idFlaws = 0;
    NPC ownerNPC;

    private void Start()
    {
        npcManager = FindObjectOfType<NPCManager>();
        deskJobM = FindObjectOfType<DeskJobManager>();
    }

    public void SetOwner(NPC owningNPC)
    {
        ownerNPC = owningNPC;
    }

    public NPC GetOwner()
    {
        return ownerNPC;
    }

    public void ChangeName(string n)
    {
        nameTxt.text = n;
    }

    public void ChangePic(Sprite p)
    {
        idPic.sprite = p;
    }

    public void ChangeIDNumber(string s)
    {
        idNumberTxt.text = s;
    }

    public void ChangeGender(NPCManager.Gender g)
    {
        genderTxt.text = g.ToString();
    }

    public void ChangeAge(int v)
    {
        ageTxt.text = v.ToString();
    }

    public void ChangeHeight(float h)
    {
        heightTxt.text = h.ToString();
    }

    public void ChangeExpiry(System.DateTime v)
    {
        expiryTxt.text = v.Date.ToShortDateString();
    }

    public string GetName()
    {
        return nameTxt.text;
    }

    public string GetIDNumber()
    {
        return idNumberTxt.text;
    }

    public void UpdateApprovalStatus(DeskJobManager.ApprovalStatus newApprovalStatus)
    {
        approvalStatus = newApprovalStatus;
        //print("approved = " + approvalStatus);
        GiveBackPapers();
        if (approvalStatus == DeskJobManager.ApprovalStatus.Rejected)
        {
            deskJobM.RejectNPC();
        }
        else
        {
            deskJobM.ApproveNPC();
        }
    }

    public NPCManager.Gender GetGender()
    {
        if (genderTxt.text == "F")
        {
            return NPCManager.Gender.F;
        }
        else
        {
            return NPCManager.Gender.M;
        }
    }

    void GiveBackPapers()
    {
        ownerNPC.Hold(transform);
        ownerNPC.Hold(ownerNPC.mySkillsCard.transform);
    }

    public int GetAge()
    {
        return int.Parse(ageTxt.text);
    }

    public float GetHeight()
    {
        return float.Parse(heightTxt.text);
    }

    public System.DateTime GetExpiry()
    {
        return System.DateTime.Parse(expiryTxt.text);
    }

    public Sprite GetPicture()
    {
        return idPic.sprite;
    }

    public void SetPlayerIsHolding(bool v)
    {
        playerIsHolding = v;
    }

    public bool GetPlayerIsHolding()
    {
        return playerIsHolding;
    }

    public void UpdateIDFlaws(int v = 0)
    {
        idFlaws += v;
    }

    public int GetIDFlaws()
    {
        return idFlaws;
    }

}
