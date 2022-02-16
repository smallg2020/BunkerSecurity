using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour
{

    DeskJobManager deskJobM;

    private void Start()
    {
        deskJobM = FindObjectOfType<DeskJobManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bell"))
        {
            deskJobM.CallNextNPC();
        }
    }
}
