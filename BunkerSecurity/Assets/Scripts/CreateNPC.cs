using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "Create NPC")]
public class CreateNPC : ScriptableObject
{
    public string NPCName = "NPC";
    public GameObject modelGO;
    public Sprite pictureID;
    public string iDNumber = "SW001F";
    public NPCManager.Gender gender = NPCManager.Gender.M;
    public int age = 35;
    public float height = 5.7f;



}
