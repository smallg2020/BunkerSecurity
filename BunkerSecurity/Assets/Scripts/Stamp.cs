using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stamp : MonoBehaviour
{
    [SerializeField]
    DeskJobManager.ApprovalStatus approvalStatus = DeskJobManager.ApprovalStatus.None;
    [SerializeField]
    TextMeshPro stampTxt;
    [SerializeField]
    Color32 noInkColour;
    bool hasInk = false;
    Color32 inkColour;
    [SerializeField]
    LayerMask stampLayers;

    public void InkStamp(Color32 newColour)
    {
        inkColour = newColour;
        stampTxt.color = inkColour;
        hasInk = true;
    }

    void RemoveInk()
    {
        inkColour = noInkColour;
        stampTxt.color = inkColour;
        hasInk = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("stamping " + other.gameObject.name);
        if (other.CompareTag("Ink"))
        {
            if (other.TryGetComponent(out Ink ink))
            {
                InkStamp(ink.inkColour);
            }
        }
        else if (other.CompareTag("Stampable"))
        {
            if (!hasInk)
                return;

            IDCard idCard = other.GetComponentInParent<IDCard>();
            if (idCard)
            {
                idCard.approvalStatus = approvalStatus;
            }
            StampText(other);
            RemoveInk();
        }
    }

    void StampText(Collider stampingCol)
    {
        GameObject so = stampingCol.gameObject;
        GameObject st = Instantiate(stampTxt.gameObject, so.transform);
        st.transform.position = stampingCol.ClosestPoint(transform.position);
        st.transform.rotation = stampTxt.transform.rotation;
        //st.transform.rotation = stampingCol.transform.rotation;
        //st.transform.Rotate(transform.forward * 180, Space.World);
        st.transform.position += st.transform.forward * -0.001f;
    }
}
