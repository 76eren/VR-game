using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class gun : MonoBehaviour
{
    [SerializeField] Material red;
    [SerializeField] Material blue;
    [SerializeField] Transform gunTip;
    [SerializeField] Transform gunTip_2;
    List<GameObject> objects = new List<GameObject>();
    [SerializeField] LineRenderer lr;
    
    [SerializeField] float extraShootingForce;
    private bool stopShooting = false;
    UnityEngine.XR.InputDevice rightController;
    UnityEngine.XR.InputDevice leftController;


    [SerializeField] HandManager handManager;
    [SerializeField] XRInteractorLineVisual XRInteractorLineVisualLeft;
    [SerializeField] XRInteractorLineVisual XRInteractorLineVisualRight;


   
    void Start()
    {
        StartCoroutine(InitializeDevices());
    }

    // This function initializes the devices
    // It appears not all devices load instantly so we just loop like this
    IEnumerator InitializeDevices()
    {
        // Fuck Oculus
        // https://forum.unity.com/threads/inputdevices-deviceconnected-does-not-fire-on-quest.823182/#post-5451048
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);
        while (devices.Count < 3)
        {
            yield return wait;
            InputDevices.GetDevices(devices);
        }
        foreach (InputDevice device in devices)
        {
            Debug.Log(string.Format("Device name '{0}' has characteristics '{1}'", device.name, device.characteristics.ToString()));

            // I actually don't know any other way to check for this lol
            if (device.characteristics.ToString().ToLower().Contains("left"))
            {
                print("Found left controller");
                leftController = device;
            }
            if (device.characteristics.ToString().ToLower().Contains("right"))
            {
                print("Found right controller");
                rightController = device;
            }
        }
    }


    private void Update()
    {
        bool l = false;
        bool r = false;
        if (handManager.getHeldItemLeft() == this.gameObject)
        {
            XRInteractorLineVisualLeft.enabled = false;
            l = true;
        }
        if (handManager.getHeldItemRight() == this.gameObject)
        {
            r = true;
            XRInteractorLineVisualRight.enabled = false;
        }
        if (!l && !r)
        {
            lr.enabled = false;
            // If the gun is not being held in any of the hands there is no point in continuing to raycast.
            // This'll save some resources (I hope)
            return; 
        }
        else
        {
            lr.enabled = true;
        }


        bool hitSomething = true;
        RaycastHit hit;
        if (Physics.Raycast(gunTip.transform.position, gunTip_2.position - gunTip.position, out hit))
        {
            lr.SetPosition(0, gunTip.position);
            lr.SetPosition(1, hit.point);
            if (hit.collider.gameObject.GetComponent<Rigidbody>() != null && hit.collider.gameObject.GetComponent<XRGrabInteractable>() != null)
            {
                if (hit.collider.gameObject.GetComponent<Renderer>() != null)
                {
                    
                    // Changes the material when highlighted
                    if (hit.collider.gameObject != this.gameObject && hit.collider.gameObject.GetComponent<Materialhandler>() != null)
                    {
                        hit.collider.gameObject.GetComponent<Materialhandler>().setHighlightedMaterial();
                        objects.Add(hit.transform.gameObject);            
                    }

                    // Pressing the shoot button
                    bool value;
                    if (rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out value) 
                        && value 
                            && handManager.getHeldItemRight() == this.gameObject
                        ||
                        leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out value)
                        && value
                            && handManager.getHeldItemLeft() == this.gameObject)
                    {
                        if (!stopShooting)
                        {
                            // We add a force on the object if we press the fire button
                            // Also we make sure that we don't keep shooting on the object of interest
                            Vector3 forceDirection = hit.point - gunTip.position;
                            hit.rigidbody.AddForce(forceDirection.normalized * extraShootingForce);
                            stopShooting = true;
                            
                        }
                    }                   
                    else
                    {
                        stopShooting = false;

                    }
                }
            }
        }
        else
        {
            hitSomething = false;
            lr.enabled = false;
        }


        List<GameObject> toRemove = new List<GameObject>();
        foreach (GameObject i in objects)
        {
            if (i != null && hit.collider != null)
            {
                if (i.name != hit.collider.name || hitSomething == false)
                {
                    i.GetComponent<Materialhandler>().setOriginalMaterial();
                    toRemove.Add(i);
                }
            }

        }
        foreach (GameObject i in toRemove)
        {
            objects.Remove(i);
        }

    }
}

class ColouredObject
{
    public GameObject target;

    public ColouredObject(GameObject target)
    {
        this.target = target;
    }
}
