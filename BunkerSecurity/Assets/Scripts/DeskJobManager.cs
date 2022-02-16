using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DeskJobManager : MonoBehaviour
{

    [SerializeField]
    int mistakesMade;
    public Transform idPosT, idPos2T, idHandPosT;
    public enum ApprovalStatus { Approved, Rejected, None }
    [SerializeField]
    Transform deskNPCT, acceptNPCT, rejectNPCT;

    SkillsManager skillsManager;
    GameManager gameManager;
    NPCManager npcManager;
    Computer computer;

    GameObject currentNPC, prevNPC;
    GameObject currentID, currentSkillsCard;
    [SerializeField]
    GameObject messagesPage;
    NPC npcScript;
    Scanner scanner;
    bool shiftStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        skillsManager = FindObjectOfType<SkillsManager>();
        npcManager = FindObjectOfType<NPCManager>();
        scanner = FindObjectOfType<Scanner>();
        computer = FindObjectOfType<Computer>();

        //restore the game status here
        skillsManager.SetCurrentScience(gameManager.currentScience);
        skillsManager.SetCurrentMilitary(gameManager.currentMilitary);
        skillsManager.SetCurrentFoodProduction(gameManager.currentFoodProduction);

        computer.OpenPage(messagesPage);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CallNextNPC();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!currentNPC)
                return;

            scanner.ScanNPC(npcScript);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ApproveNPC();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            RejectNPC();
        }
    }

    public void ApproveNPC()
    {
        if (!currentNPC)
            return;

        GiveBackID();
        npcScript.SetDest(acceptNPCT);
        prevNPC = currentNPC;
        currentNPC = null;
    }

    public void RejectNPC()
    {
        if (!currentNPC)
            return;

        GiveBackID();
        npcScript.SetDest(rejectNPCT);
        prevNPC = currentNPC;
        currentNPC = null;
    }

    public void StartShift()
    {
        if (shiftStarted)
            return;

        StartCoroutine(WorkingDeskJob());
    }

    IEnumerator WorkingDeskJob()
    {
        shiftStarted = true;
        while (shiftStarted)
        {
            if (currentNPC)
                yield return null;

            CallNextNPC();
            yield return new WaitForSeconds(1);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }



    public void CallNextNPC()
    {
        if (currentNPC)
            return;

        int idflaws = 0;
        if (Random.Range(0, 100) < gameManager.idFlawChance)
        {
            idflaws = Random.Range(1, gameManager.idFlaws);
        }
        int skillflaws = 0;
        if (Random.Range(0, 100) < gameManager.skillsFlawChance)
        {
            skillflaws = Random.Range(1, gameManager.skillsFlaws);
        }
        currentNPC = npcManager.CreateNPC(idflaws, skillflaws);
        npcScript = currentNPC.GetComponent<NPC>();
        currentID = npcScript.myIDCard;
        currentSkillsCard = npcScript.mySkillsCard;
        npcScript.SetDest(deskNPCT);
    }

    public void GiveBackID()
    {
        npcScript.Hold(currentID.transform);
        npcScript.Hold(currentSkillsCard.transform);
    }

    public Transform GetRejectionRoomT()
    {
        return rejectNPCT;
    }

    public Transform GetApprovedRoomT()
    {
        return acceptNPCT;
    }

    public void UpdateMistakesMade(int v)
    {
        mistakesMade += v;
    }

    public int GetMistakesMade()
    {
        return mistakesMade;
    }
}
