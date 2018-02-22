using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Needed //////////////////////////////////////////////////
using HoloLensXboxController;  // much easier to use than the toolkit's
///////////////////////////////////////////////////////////

public class Player : MonoBehaviour {

    // cameras setup by set designers in the file
    // we will need their positions and oritentations
    public Camera[] SceneCameras;

    public bool isUsingSetCams = true;
    public bool isXboxController = true; // for controller mode
    public int camNum = 0; // current camera ID

    ControllerInput controllerInput; // controllerInput


    // Use this for initialization
    void Start () {

        // Needed //////////////////////////////////////////////////
        controllerInput = new ControllerInput(0, 0.19f);
        // First parameter is the number, starting at zero, of the controller you want to follow.
        // Second parameter is the default “dead” value; meaning all stick readings less than this value will be set to 0.0.
        ///////////////////////////////////////////////////////////

        BuildCamerasArray();
        InitSetFirstCamera();


        StartCoroutine(NextCamDelay());//Testing Only

    }

    // Update is called once per frame
    void Update () {


        // Needed //////////////////////////////////////////////////
        controllerInput.Update();
        ///////////////////////////////////////////////////////////

        if (isXboxController)
        {
            // RB: teleport to next camera
            if (controllerInput.GetButtonDown(ControllerButton.RightShoulder))
                NextCam();

            if (controllerInput.GetButtonDown(ControllerButton.LeftShoulder))
                PreviousCam();

            if (controllerInput.GetButtonDown(ControllerButton.X))
                isUsingSetCams = !isUsingSetCams;
        }
    }

    public void NextCam()
    {
        if (isUsingSetCams)
        {
            for (int i = 0; i < SceneCameras.Length; i++)
            {
                camNum++;
                if (camNum >= SceneCameras.Length)
                {
                    camNum = 0;
                }
                if (SceneCameras[camNum].gameObject != gameObject)
                {
                    transform.position = SceneCameras[camNum].gameObject.transform.position;
                    //transform.rotation = SceneCameras[camNum].gameObject.transform.rotation;
                    break;
                }
            }
        }

        else
        {
            transform.position = new Vector3(5, 1, -1.64f);
        }
        
    }
    public void PreviousCam()
    {
        if (isUsingSetCams)
        {
            for (int i = 0; i < SceneCameras.Length; i++)
            {
                camNum--;
                if (camNum < 0)
                {
                    camNum = SceneCameras.Length - 1;
                }
                if (SceneCameras[camNum].gameObject != gameObject)
                {
                    transform.position = SceneCameras[camNum].gameObject.transform.position;
                    //transform.rotation = SceneCameras[camNum].gameObject.transform.rotation;
                    break;
                }
            }
        }
        else
        {
            transform.position = new Vector3(2, 1, 1);
        }
    }

    void InitSetFirstCamera()
    {
        for (int i = 0; i < SceneCameras.Length; i++)
        {
            //if (SceneCameras[i].gameObject != gameObject)
            if (SceneCameras[i].gameObject.tag != "MainCamera")
            {
                transform.position = SceneCameras[i].gameObject.transform.position;

                camNum = i;
                break;
            }
        }
        //Turn Off All Cams but Main
        for (int i = 0; i < SceneCameras.Length; i++)
        {
            //if (SceneCameras[i].gameObject != Camera.main)
            if (SceneCameras[i].gameObject.tag != "MainCamera")
            {
                SceneCameras[i].enabled = false;
                //SceneCameras[i].GetComponent<AudioListener>().enabled = false;
            }
        }
    }
    
    void BuildCamerasArray()
    {
        SceneCameras = GameObject.FindObjectsOfType<Camera>();
    }


    IEnumerator NextCamDelay()
    {

        yield return new WaitForSeconds(5);

        NextCam();


        yield return new WaitForSeconds(5);

        NextCam();

        yield return new WaitForSeconds(5);

        PreviousCam();

        yield return new WaitForSeconds(5);

        PreviousCam();

    }
}
