using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CheckGrab : XRRayInteractor
{
    [SerializeField]
    bool leftHand, rightHand;

    public void OnSelectEnter(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.TryGetComponent<IDCard>(out IDCard iDCard))
        {
            iDCard.SetPlayerIsHolding(true);
        }
        if (args.interactableObject.transform.TryGetComponent<GrabAttachPoints>(out GrabAttachPoints gaps))
        {
            if (leftHand)
            {
                gaps.myGrabInteractable.attachTransform = gaps.leftHandAttachT;

            }
            else if (rightHand)
            {
                gaps.myGrabInteractable.attachTransform = gaps.rightHandAttachT;
            }
            else
            {
                gaps.myGrabInteractable.attachTransform = gaps.nonHandAttachT;
            }
        }
    }


    public void OnSelectExit(SelectExitEventArgs args)
    {
        if (args.interactableObject.transform.TryGetComponent(out GrabAttachPoints gaps))
            gaps.myGrabInteractable.attachTransform = gaps.nonHandAttachT;
        if (args.interactableObject.transform.TryGetComponent(out IDCard iDCard))
        {
            iDCard.SetPlayerIsHolding(false);
        }
    }

}
