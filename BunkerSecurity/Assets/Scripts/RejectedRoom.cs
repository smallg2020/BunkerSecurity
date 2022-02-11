using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RejectedRoom : MonoBehaviour
{
    DeskJobManager deskJobM;
    // Start is called before the first frame update
    void Start()
    {
        deskJobM = FindObjectOfType<DeskJobManager>();
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
            if (npcScript.GetTotalFlaws() == 0)
            {
                deskJobM.UpdateMistakesMade(1);
            }
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(g);
    }
}
