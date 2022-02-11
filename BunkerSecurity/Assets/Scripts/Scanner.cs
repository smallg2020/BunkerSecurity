using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField]
    Computer computer;
    [SerializeField]
    float rayDist = 5;
    [SerializeField]
    LayerMask scanLayers;
    [SerializeField]
    Transform rayStartT;
    [SerializeField]
    AudioSource scanSound;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Scan();
        }
    }

    public void Scan()
    {
        //print("starting scan");
        RaycastHit hit;
        Ray ray = new Ray(rayStartT.position, rayStartT.forward);
        if (Physics.Raycast(ray, out hit, rayDist, scanLayers))
        {
            if (!scanSound.isPlaying)
            {
                scanSound.Play();
            }
            //print("scanning " + hit.collider.transform.name);
            if (hit.collider.TryGetComponent<NPC>(out NPC npcInfo))
            {
                ScanNPC(npcInfo);
            }
        }
    }

    public void ScanNPC(NPC npcInfo)
    {
        computer.npcNameTxt.text = npcInfo.myName;
        computer.npcIDNumberTxt.text = npcInfo.iDNumber;
        computer.npcGenderTxt.text = npcInfo.gender.ToString();
        computer.npcAgeTxt.text = npcInfo.age.ToString();
        computer.npcHeightTxt.text = npcInfo.height.ToString();
        computer.SetNPCInfoPic(npcInfo.pictureID);
    }


}
