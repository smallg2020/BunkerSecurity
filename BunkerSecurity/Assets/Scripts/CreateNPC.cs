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
    public Gender gender = Gender.Male;
    public int age = 35;
    public float height = 5.7f;


    public enum Gender { Male, Female }
}
