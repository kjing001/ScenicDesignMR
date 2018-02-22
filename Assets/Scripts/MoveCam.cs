using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Needed //////////////////////////////////////////////////
using HoloLensXboxController;
///////////////////////////////////////////////////////////
public class MoveCam : MonoBehaviour {







    // Use this for initialization
    void Start () {





        StartCoroutine(NextCamDelay());//Testing Only

    }

    // Update is called once per frame
    void Update () {
		
	}



    public void ResetCam()
    {
        transform.position = new Vector3(0, 0, 0);

    }

    IEnumerator NextCamDelay()
    {
       
        yield return new WaitForSeconds(5);

        transform.position = new Vector3(5, 1, -1.64f);

       
        yield return new WaitForSeconds(5);

        transform.position = new Vector3(2, 1, 1);

        yield return new WaitForSeconds(5);

        transform.position = new Vector3(5, 1, -1.5f);

        yield return new WaitForSeconds(5);

        transform.position = new Vector3(2, 1, 1);

    }
}
