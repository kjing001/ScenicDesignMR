using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Needed //////////////////////////////////////////////////
using HoloLensXboxController;
///////////////////////////////////////////////////////////

public class CameraShiftController : MonoBehaviour
{
    public Camera[] SceneCameras;
    public GameObject CPlayer;
    public int camNum = 0;

    // Needed //////////////////////////////////////////////////
    private ControllerInput controllerInput;
    ///////////////////////////////////////////////////////////

    // Use this for initialization
    void Start()
    {
        if (!CPlayer)
        { CPlayer = gameObject; }

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
    void Update()
    {

        // Needed //////////////////////////////////////////////////
        controllerInput.Update();
        ///////////////////////////////////////////////////////////

        // if RT next, LT previous
        if (controllerInput.GetAxisRightTrigger() > 0.55)
        {
            NextCam();
        }
        if (controllerInput.GetAxisLeftTrigger() > 0.55)
        {
            PreviousCam();
        }


    }
    IEnumerator NextCamDelay()
    {
        yield return new WaitForSeconds(3);
        MoveCam();


        //while (true)
        //{
        //    yield return new WaitForSeconds(3);
        //    NextCam();
        //}
    }

    public void MoveCam()
    {
        gameObject.transform.position = new Vector3(0, 0, 0);

    }


    public void NextCam()
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
                CPlayer.transform.position = SceneCameras[camNum].gameObject.transform.position;
                break;
            }
        }
    }
    public void PreviousCam()
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
                CPlayer.transform.position = SceneCameras[camNum].gameObject.transform.position;

                break;
            }
        }
    }
    void InitSetFirstCamera()
    {
        for (int i = 0; i < SceneCameras.Length; i++)
        {
            if (SceneCameras[i].gameObject != gameObject)
            {
                CPlayer.transform.position = SceneCameras[i].gameObject.transform.position;

                camNum = i;
                break;
            }
        }
        //Turn Off All Cams but Main
        for (int i = 0; i < SceneCameras.Length; i++)
        {
            if (SceneCameras[i].gameObject != gameObject)
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

}
