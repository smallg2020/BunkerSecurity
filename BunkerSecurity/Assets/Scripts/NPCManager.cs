using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public Color[] infectionLevelColour;
    public System.DateTime currentDate = System.DateTime.Today;
    public int minNPCAge, maxNPCAge;
    public float minNPCHeight, maxNPCHeight;
    public int minExpiryDate, maxExpiryDate;

    public string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public enum Gender { M, F }

    NPCCreator npcCreator;


    // Start is called before the first frame update
    void Start()
    {
        npcCreator = FindObjectOfType<NPCCreator>();
    }

    public GameObject CreateNPC(int idflaws = 0, int skillflaws = 0)
    {
        return npcCreator.CreateNewNPC(idflaws, skillflaws);
    }


}
