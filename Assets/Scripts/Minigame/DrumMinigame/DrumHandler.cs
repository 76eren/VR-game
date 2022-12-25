using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DrumHandler : MonoBehaviour
{

    private Materialhandler materialhandler;
    public AudioSource source;
    public AudioClip clip;

    private GameObject myStick;
    [SerializeField] private DrumGameHandler drumGameHandler;
    

    private void Start()
    {
        materialhandler = GetComponent<Materialhandler>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "stick")
        {
            // Makes sure we don't trigger multiple drums with one stick
            bool found = false;
            foreach (GameObject i in drumGameHandler.getDrums())
            {
                if (i.GetComponent<DrumHandler>().getStick() == other.gameObject)
                {
                    found = true;
                }
            }

            if (!found)
            {
                materialhandler.setHighlightedMaterial();
                source.PlayOneShot(clip);
                this.setStick(other.gameObject);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "stick")
        {
            materialhandler.setOriginalMaterial();
            myStick = null;
        }
    }

    private void setStick(GameObject myStick)
    {
        this.myStick = myStick;
    }

    public GameObject getStick()
    {
        return this.myStick;
    }

}
