using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApprovedRoom : MonoBehaviour
{
    DeskJobManager deskJobM;
    // Start is called before the first frame update
    void Start()
    {
        deskJobM = FindObjectOfType<DeskJobManager>();
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
            }
        }
        yield return new WaitForSeconds(0.2f);
        Destroy(npc);
    }
}
