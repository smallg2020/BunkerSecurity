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

    GameManager gameManager;
    NPCManager npcManager;

    GameObject currentNPC, prevNPC;
    GameObject currentID;
    NPC npcScript;
    Scanner scanner;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        npcManager = FindObjectOfType<NPCManager>();
        scanner = FindObjectOfType<Scanner>();

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
            if (!currentNPC)
                return;

            GiveBackID();
            npcScript.ApproveNPC();
            npcScript.SetDest(acceptNPCT);
            prevNPC = currentNPC;
            currentNPC = null;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!currentNPC)
                return;

            GiveBackID();
            npcScript.RejectNPC();
            npcScript.SetDest(rejectNPCT);
            prevNPC = currentNPC;
            currentNPC = null;
        }
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
        npcScript.SetDest(deskNPCT);
    }

    void GiveBackID()
    {
        npcScript.Hold(currentID.transform);
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
