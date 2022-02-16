using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RejectedRoom : MonoBehaviour
{
    [SerializeField]
    GameObject messagesPage;

    DeskJobManager deskJobM;
    Messages messages;
    Computer computer;

    // Start is called before the first frame update
    void Start()
    {
        deskJobM = FindObjectOfType<DeskJobManager>();
        messages = FindObjectOfType<Messages>();
        computer = FindObjectOfType<Computer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        StartCoroutine(DestroyNPC(other.gameObject));
    }

    IEnumerator DestroyNPC(GameObject g)
    {
        NPC npcScript = g.GetComponent<NPC>();
        if (npcScript)
        {
            if (computer.showingInfoForNPC == npcScript.gameObject)
            {
                computer.ResetNPCInfo();
            }

            if (npcScript.GetTotalFlaws() == 0)
            {
                deskJobM.UpdateMistakesMade(1);
                string mt = "Rejected an acceptable person!";
                messages.SendNewMessage(mt);
                computer.OpenPage(messagesPage);
            }
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(g);
    }
}
