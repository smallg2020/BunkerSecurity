using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApprovedRoom : MonoBehaviour
{
    [SerializeField]
    GameObject messagesPage, skillsPage;

    [SerializeField]
    SkillsManager skillsManager;
    DeskJobManager deskJobM;
    Messages messages;
    [SerializeField]
    Computer computer;

    // Start is called before the first frame update
    void Start()
    {
        deskJobM = FindObjectOfType<DeskJobManager>();
        messages = FindObjectOfType<Messages>();
        computer = FindObjectOfType<Computer>();
    }

    public void OnTriggerEnter(Collider other)
    {
        StartCoroutine(ApproveNPC(other.gameObject));
    }

    IEnumerator ApproveNPC(GameObject npc)
    {
        if (npc.TryGetComponent(out NPC npcScript))
        {
            if (npcScript.GetTotalFlaws() > 0)
            {
                deskJobM.UpdateMistakesMade(1);
                string mt = "Invalid ID!";
                messages.SendNewMessage("mt");
                computer.OpenPage(messagesPage);

            }
            else
            {
                computer.OpenPage(skillsPage);
                print("updating science +" + npcScript.myScienceSkill);
                skillsManager.UpdateCurrentScience(npcScript.myScienceSkill);
                print("new science = " + skillsManager.GetCurrentScience());
                skillsManager.UpdateCurrentMilitary(npcScript.myMilitarySkill);
                skillsManager.UpdateCurrentFoodProduction(npcScript.myFoodSkill);
            }

            if (computer.showingInfoForNPC == npcScript.gameObject)
                computer.ResetNPCInfo();
        }
        yield return new WaitForSeconds(0.2f);
        Destroy(npc);
    }
}
