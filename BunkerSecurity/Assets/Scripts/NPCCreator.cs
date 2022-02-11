using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class NPCCreator : MonoBehaviour
{
    public int changeHairChance = 100, changeHatChance = 5, changeGlassesChance = 25, sunglassesChance = 25;
    [SerializeField]
    GameObject[] males, females;

    [SerializeField]
    Material[] hairColours, eyeColours, hatColours, glassesColours;

    [SerializeField]
    TextAsset maleNames, femaleNames;

    [SerializeField]
    GameObject idCardGO, skillsCardGO;

    [SerializeField]
    Transform dressingRoomT;

    [SerializeField]
    Camera idCamera;

    NPCManager npcManager;
    Sprite sss;

    private void Start()
    {
        npcManager = FindObjectOfType<NPCManager>();
    }

    public GameObject CreateNewNPC(int idFlaws = 0, int skillFlaws = 0)
    {
        GameObject npc = GenerateNPC();
        GameObject id = CreateNPCIDCard(npc);
        GameObject sc = CreateNPCSkillsCard(npc);
        NPC npcScript = npc.GetComponent<NPC>();
        npcScript.myIDCard = id;
        npcScript.mySkillsCard = sc;
        npcScript.idScript = id.GetComponent<IDCard>();
        npcScript.Hold(id.transform);
        FillNPCIDCard(npc, id, idFlaws);
        SkillsCard scScript = sc.GetComponent<SkillsCard>();
        FillNPCSkillsCard(npcScript, scScript, skillFlaws);
        npcScript.Hold(sc.transform);
        //npcScript.UpdateFlaws(flaws);
        return npc;
    }

    //this info is always correct as it stores the base stats for the NPC
    public GameObject GenerateNPC()
    {
        GameObject npc;
        bool isMale = Random.Range(0, 2) == 0 ? true : false;
        if (isMale)
        {
            npc = Instantiate(males[Random.Range(0, males.Length)], dressingRoomT);
        }
        else
        {
            npc = Instantiate(females[Random.Range(0, females.Length)], dressingRoomT);
        }
        NPC npcScript = npc.GetComponent<NPC>();
        string newName = GetNewName(isMale);
        npcScript.ChangeName(newName);
        //change NPC appearance
        npcScript.ChangeBody();
        int change = Random.Range(0, 101);
        if (change <= changeHairChance)
        {
            int id = Random.Range(0, hairColours.Length);
            npcScript.ChangeHair(hairColours[id]);
        }
        else
        {
            npcScript.ChangeHair(null);
        }
        npcScript.ChangeEyes(eyeColours[Random.Range(0, eyeColours.Length)]);
        change = Random.Range(0, 101);
        if (change <= changeGlassesChance)
        {
            int id = Random.Range(0, glassesColours.Length);
            change = Random.Range(0, 101);
            bool hasSunGlasses = false;
            if (change <= sunglassesChance)
            {
                hasSunGlasses = true;
            }
            npcScript.ChangeGlassesColour(glassesColours[id], hasSunGlasses);
        }
        else
        {
            npcScript.ChangeGlassesColour(null);
        }
        change = Random.Range(0, 101);
        if (change <= changeHatChance)
        {
            int id = Random.Range(0, hatColours.Length);
            npcScript.ChangeHat(hatColours[id]);
        }
        else
        {
            npcScript.ChangeHat(null);
        }

        npc.transform.name = newName;
        npcScript.iDNumber = GenerateNewIDNumber(newName);
        float height = Random.Range(npcManager.minNPCHeight, npcManager.maxNPCHeight);
        npc.transform.localScale = new Vector3(1, height, 1);
        npcScript.age = Random.Range(npcManager.minNPCAge, npcManager.maxNPCAge + 1);
        if (isMale)
        {
            npcScript.gender = NPCManager.Gender.M;
        }
        else
        {
            npcScript.gender = NPCManager.Gender.F;
        }
        double h = System.Math.Round(6 * height, 2);
        npcScript.height = (float)h;
        npcScript.myScienceSkill = Random.Range(0f, 1f);
        npcScript.myMilitarySkill = Random.Range(0f, 1f);
        npcScript.myFoodSkill = Random.Range(0f, 1f);
        return npc;
    }

    string GetNewName(bool isMale = true)
    {
        //get a random name from file
        string[] lines;
        string path = "";
        if (isMale)
        {
            path = "Assets/Resources/MaleNames.txt";
        }
        else
        {
            path = "Assets/Resources/FemaleNames.txt";
        }
        lines = System.IO.File.ReadAllLines(path);
        string newName = lines[Random.Range(0, lines.Length)];
        return newName;
    }

    string GenerateNewIDNumber(string n)
    {
        string s = n.Substring(0, 1) + GetRandomLetter();
        for (int i = 0; i < 3; i++)
        {
            s += Random.Range(0, 10).ToString();
        }
        for (int i = 0; i < 2; i++)
        {
            s += GetRandomLetter();
        }
        return s;
    }

    string GetRandomLetter()
    {
        string s = npcManager.letters[Random.Range(0, npcManager.letters.Length)].ToString();
        return s;
    }

    GameObject CreateNPCIDCard(GameObject npc)
    {
        GameObject id = Instantiate(idCardGO, npc.transform);
        id.transform.position += Vector3.up * 0.01f;
        return id;
    }

    GameObject CreateNPCSkillsCard(GameObject npc)
    {
        GameObject sc = Instantiate(skillsCardGO, npc.transform);
        sc.transform.position += Vector3.up * 0.01f;
        return sc;
    }

    string ChangeName(IDCard id)
    {
        string newName = "";
        do
        {
            newName = GetNewName();
        } while (newName == id.GetName());
        return newName;
    }

    System.DateTime ChangeDate(int v)
    {
        System.DateTime nd = npcManager.currentDate.AddDays(v);
        return nd;
    }

    void TakePic()
    {
        int width = idCamera.targetTexture.width;
        int height = idCamera.targetTexture.height;
        int depth = idCamera.targetTexture.depth;
        Rect rect = new Rect(0, 0, width, height);
        idCamera.enabled = true;
        RenderTexture renderTexture = idCamera.targetTexture;
        idCamera.Render();
        Texture2D tex = new Texture2D(width, height);
        RenderTexture currentRenderTexture = RenderTexture.active;
        RenderTexture.active = renderTexture;
        tex.ReadPixels(rect, 0, 0);
        tex.Apply();
        RenderTexture.active = currentRenderTexture;
        sss = Sprite.Create(tex, new Rect(0, 0, width, height), Vector2.zero, 100);
        idCamera.enabled = false;
    }

    void QuickAppearanceChange(GameObject npc, IDCard iDCard)
    {
        npc.transform.position -= Vector3.up * 5;
        GameObject pp = GenerateNPC();
        TakePic();
        iDCard.ChangePic(sss);
        Destroy(pp);
        npc.transform.position += Vector3.up * 5;
        iDCard.transform.position = npc.transform.position;
        iDCard.transform.position += Vector3.up * 1f;
    }

    void FillNPCIDCard(GameObject npc, GameObject id, int flaws = 0)
    {
        NPC npcScript = npc.GetComponent<NPC>();
        TakePic();
        IDCard iDCard = id.GetComponent<IDCard>();
        //we also need to store the valid picture to the NPC data here as it's not created before this point
        npcScript.pictureID = sss;

        //for starters just make all info correct
        iDCard.ChangePic(sss);
        iDCard.ChangeName(npcScript.myName);
        iDCard.ChangeIDNumber(npcScript.iDNumber);
        iDCard.ChangeGender(npcScript.gender);
        iDCard.ChangeAge(npcScript.age);
        iDCard.ChangeHeight(npcScript.height);
        int days = Random.Range(0, npcManager.maxExpiryDate);
        System.DateTime d = ChangeDate(days);
        iDCard.ChangeExpiry(d);

        CreateIDCardFlaws(npc, iDCard, flaws);
    }

    /// <summary>
    /// fills all info in correctly at beginning
    /// we can create flaws later with the createflaws function
    /// </summary>
    /// <param name="npc"></param>
    /// <param name="sc"></param>
    void FillNPCSkillsCard(NPC npc, SkillsCard sc, int skillFlaws = 0)
    {
        sc.SetIDPic(npc.pictureID);
        sc.SetName(npc.myName);
        sc.SetIDNumber(npc.iDNumber);
        sc.SetScienceSkill(npc.myScienceSkill);
        sc.SetMilitarySkill(npc.myMilitarySkill);
        sc.SetFoodSkill(npc.myFoodSkill);

        CreateSkillsCardFlaws(npc.gameObject, sc, skillFlaws);
    }

    void CreateIDCardFlaws(GameObject npc, IDCard iDCard, int flaws = 0)
    {
        bool[] alreadyChanged = new bool[7];
        int info, createdFlaws = 0;
        for (int i = 0; i < flaws; i++)
        {
            int tries = 0;
            do
            {
                info = Random.Range(0, 7);
                tries++;
            } while (alreadyChanged[info] && tries < 50);
            //print("changing info " + info);
            if (alreadyChanged[info])
            {
                //print("already changed");
            }
            else
            {
                if (info == 0)
                {
                    //change the pic
                    //print("changed id pic");
                    QuickAppearanceChange(npc, iDCard);
                }
                else if (info == 1)
                {
                    //print("changed id name");
                    //change the name                
                    iDCard.ChangeName(ChangeName(iDCard));
                }
                else if (info == 2)
                {
                    //print("changed id number");
                    //change id number
                    iDCard.ChangeIDNumber(GenerateNewIDNumber(ChangeName(iDCard)));
                }
                else if (info == 3)
                {
                    //print("changed age");
                    //change age
                    int newAge = 0;
                    do
                    {
                        newAge = Random.Range(npcManager.minNPCAge, npcManager.maxNPCAge);
                    } while (newAge == iDCard.GetAge());
                    iDCard.ChangeAge(newAge);
                }
                else if (info == 4)
                {
                    //print("changed height");
                    //change the height
                    float newHeight = 0.00f;
                    do
                    {
                        newHeight = 6 * Random.Range(npcManager.minNPCHeight, npcManager.maxNPCHeight);
                        newHeight = (float)System.Math.Round(newHeight, 2);
                    } while (newHeight == iDCard.GetHeight());
                    iDCard.ChangeHeight(newHeight);
                }
                else if (info == 5)
                {
                    //print("changed gender");
                    //change the gender
                    NPCManager.Gender newGender = NPCManager.Gender.F;
                    do
                    {
                        if (Random.Range(0, 2) == 0)
                        {
                            newGender = NPCManager.Gender.M;
                        }
                        else
                        {
                            newGender = NPCManager.Gender.F;
                        }
                    } while (newGender == iDCard.GetGender());
                    iDCard.ChangeGender(newGender);
                }
                else if (info == 6)
                {
                    //print("changed expiry");
                    //change the expiry
                    System.DateTime newDate = System.DateTime.Now.AddDays(Random.Range(-1, npcManager.minExpiryDate));
                    iDCard.ChangeExpiry(newDate);
                }
                alreadyChanged[info] = true;
                createdFlaws++;
            }
        }
        iDCard.UpdateIDFlaws(createdFlaws);
    }

    void CreateSkillsCardFlaws(GameObject npc, SkillsCard sc, int flaws = 0)
    {

    }


}
