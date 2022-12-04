// This will detect which object we are holding in which hand

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;


public class HandManager : MonoBehaviour
{
    [SerializeField] XRRayInteractor lHand;
    [SerializeField] XRRayInteractor rHand;

    [SerializeField] XRInteractorLineVisual lHandLine;
    [SerializeField] XRInteractorLineVisual rHandLine;

    // Not me using depreciated functions
    public GameObject getHeldItemLeft()
    {
        if (lHand.selectTarget == null)
        {
            lHandLine.enabled = true;
            return null;
        }
        return lHand.selectTarget.gameObject;
    }

    public GameObject getHeldItemRight()
    {
        if (rHand.selectTarget == null)
        {
            return null;
        }
        return rHand.selectTarget.gameObject;
    }

    private void Update()
    {
        if (getHeldItemLeft() != null)
        {
            if (getHeldItemLeft().gameObject.GetComponent<XRGrabInteractable>() != null)
            {
                lHandLine.enabled = false;
            }
            else
            {
                lHandLine.enabled = true;

            }
        }
        else
        {
            lHandLine.enabled = true;
        }
        

        if (getHeldItemRight() != null)
        {
            if (getHeldItemRight().gameObject.GetComponent<XRGrabInteractable>() != null)
            {
                rHandLine.enabled = false;
            }
            else
            {
                rHandLine.enabled = true;
            }
        }
        else
        {
            rHandLine.enabled = true;
        }
    }

}
