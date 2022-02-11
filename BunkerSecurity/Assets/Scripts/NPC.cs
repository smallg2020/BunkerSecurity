using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Animations.Rigging;

public class NPC : MonoBehaviour
{
    public string myName = "";
    public GameObject[] myBodys;
    public MeshRenderer myHair, myEyes, myHat, myGlasses, myLenses;
    public Texture2D myIDTexture;
    public Sprite pictureID;
    public string iDNumber = "SW001F";
    public NPCManager.Gender gender = NPCManager.Gender.M;
    public int age = 35;
    public float height = 5.7f;
    public GameObject myIDCard, mySkillsCard;
    public int myFlaws;
    [Range(0, 1)]
    public float myScienceSkill, myMilitarySkill, myFoodSkill;
    public Transform rightHandT;
    public TwoBoneIKConstraint rightHandIK;
    public Transform rightHandTargetT;

    public IDCard idScript;
    NavMeshAgent ai;
    Transform myDestinationT;
    DeskJobManager deskJobManager;


    public string myState = "";

    private void Awake()
    {
        ai = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        deskJobManager = FindObjectOfType<DeskJobManager>();
    }

    private void Update()
    {
        if (myState == "moving to dest")
        {
            if (!myDestinationT)
                return;
            if (Vector3.Distance(transform.position, myDestinationT.position) > 0.2f)
                return;

            if (myDestinationT.CompareTag("Desk"))
            {
                GiveID();
                myState = "wait for approval";
            }
        }
        else if (myState == "wait for approval")
        {
            if (idScript)
            {
                if (idScript.GetPlayerIsHolding())
                {
                    MoveHandToDeskIDPos();
                    SetRightHandIKWeight(1);
                }
            }
        }
    }

    public void SetRightHandIKWeight(float v)
    {
        v = Mathf.Clamp01(v);
        rightHandIK.weight = v;
    }

    public float GetRightHandIKWeight()
    {
        return rightHandIK.weight;
    }

    public void SetRightHandTargetPos(Vector3 pos)
    {
        rightHandTargetT.position = pos;
    }

    public void SetRightHandTargetRot(Quaternion rot)
    {
        rightHandTargetT.rotation = rot;
    }

    public Transform GetRightHandTargetT()
    {
        return rightHandTargetT;
    }

    public void SetDest(Transform dest)
    {
        myDestinationT = dest;
        ai.SetDestination(dest.position);
        myState = "moving to dest";
    }

    public void Hold(Transform h)
    {
        h.localScale = Vector3.one * 0.01f;
        h.SetParent(rightHandT);
        h.position = rightHandT.position;
        h.rotation = rightHandT.rotation;
    }

    void GiveID()
    {
        StartCoroutine(GivingID());
    }

    void MoveHandToDeskIDPos()
    {
        rightHandTargetT.position = deskJobManager.idHandPosT.position;
        rightHandTargetT.rotation = deskJobManager.idHandPosT.rotation;
    }

    public void ApproveNPC()
    {
        idScript.approvalStatus = DeskJobManager.ApprovalStatus.Approved;
    }

    public void RejectNPC()
    {
        idScript.approvalStatus = DeskJobManager.ApprovalStatus.Rejected;
    }

    public void UpdateFlaws(int v)
    {
        myFlaws += v;
    }

    public int GetTotalFlaws()
    {
        return myFlaws;
    }

    IEnumerator GivingID()
    {
        MoveHandToDeskIDPos();
        rightHandIK.weight = 0;
        float t = 0;
        while (t < 0.1f)
        {
            t += Time.deltaTime;
            rightHandIK.weight += 0.1f;
            yield return null;
        }
        rightHandIK.weight = 1;
        MoveHandToDeskIDPos();
        yield return null;
        //print("holding " + rightHandT.childCount + " items");
        int holding = rightHandT.childCount;
        if (holding > 0)
        {
            for (int i = holding - 1; i > -1; i--)
            {
                Transform c = rightHandT.GetChild(i);
                c.SetParent(null);
                //print("gave " + c.name);
            }
        }
        myIDCard.transform.localScale = Vector3.one;
        mySkillsCard.transform.localScale = Vector3.one;
        yield return null;
        myIDCard.GetComponent<Rigidbody>().velocity = Vector3.zero;
        mySkillsCard.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Vector3 pos1, pos2, post1, post2;
        Quaternion rot1, rot2, rott1, rott2;
        t = 0;
        pos1 = myIDCard.transform.position;
        pos2 = deskJobManager.idPosT.position + Vector3.up * 0.2f;
        post1 = mySkillsCard.transform.position;
        post2 = deskJobManager.idPos2T.position + Vector3.up * 0.2f;
        myIDCard.transform.position = Vector3.Lerp(pos1, pos2, 0.1f);
        rot1 = myIDCard.transform.rotation;
        rot2 = deskJobManager.idPosT.rotation;
        rott1 = mySkillsCard.transform.rotation;
        rott2 = deskJobManager.idPos2T.rotation;
        yield return null;
        while (t < 0.1f)
        {
            t += Time.deltaTime;
            myIDCard.transform.position = Vector3.Lerp(pos1, pos2, t * 5);
            myIDCard.transform.rotation = Quaternion.Lerp(rot1, rot2, t * 2);
            mySkillsCard.transform.position = Vector3.Lerp(post1, post2, t * 5);
            mySkillsCard.transform.rotation = Quaternion.Lerp(rott1, rott2, t * 2);
            yield return null;
        }
        rightHandIK.weight = 0;
        t = 0;
        pos1 = myIDCard.transform.position;
        pos2 = deskJobManager.idPosT.position + Vector3.up * 0.02f;
        rot1 = myIDCard.transform.rotation;
        rot2 = deskJobManager.idPosT.rotation;
        post1 = mySkillsCard.transform.position;
        post2 = deskJobManager.idPos2T.position + Vector3.up * 0.02f;
        rott1 = mySkillsCard.transform.rotation;
        rott2 = deskJobManager.idPos2T.rotation;
        while (t < 0.1f)
        {
            t += Time.deltaTime;
            myIDCard.transform.position = Vector3.Lerp(pos1, pos2, t * 10);
            myIDCard.transform.rotation = Quaternion.Lerp(rot1, rot2, t * 10);
            mySkillsCard.transform.position = Vector3.Lerp(post1, post2, t * 10);
            mySkillsCard.transform.rotation = Quaternion.Lerp(rott1, rott2, t * 10);
            yield return null;
        }

    }



    public void ChangeName(string newName)
    {
        myName = newName;
    }

    public void ChangeBody()
    {
        int b = Random.Range(0, myBodys.Length);
        foreach (GameObject body in myBodys)
        {
            body.SetActive(false);
        }
        myBodys[b].SetActive(true);
    }

    public void ChangeHair(Material newColour)
    {
        if (newColour)
        {
            myHair.material = newColour;
        }
        else
        {
            myHair.gameObject.SetActive(false);
        }
    }

    public void ChangeEyes(Material newColour)
    {
        myEyes.material = newColour;
    }

    public void ChangeHat(Material newColour)
    {
        if (newColour)
        {
            myHat.material = newColour;
        }
        else
        {
            myHat.gameObject.SetActive(false);
        }
    }

    public void ChangeGlassesColour(Material newColour, bool sunglasses = false)
    {
        if (newColour)
        {
            myGlasses.material = newColour;
            if (sunglasses)
            {
                myLenses.material = newColour;
            }
        }
        else
        {
            myGlasses.gameObject.SetActive(false);
        }
    }
}
