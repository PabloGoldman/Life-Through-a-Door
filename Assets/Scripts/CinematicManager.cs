using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Cinemachine;


public class CinematicManager : MonoBehaviourSingleton<CinematicManager>
{
    Camera cam;

    [SerializeField] GameObject player;

    [SerializeField] FPSCameraController fpsCamera;
    [SerializeField] FPSController fpsController;


    [Header("Hidden Puzzle transitions")]
    [SerializeField] CinemachineVirtualCamera bedCam;

    [SerializeField] float rotationAngle;
    [SerializeField] float rotationDuration;

    private void Start()
    {
        cam = Camera.main;
    }

    public void FreezePlayer()
    {
        fpsCamera.enabled = false;
        fpsController.enabled = false;
    }

    public void ReanudePlayer()
    {
        fpsCamera.enabled = true;
        fpsController.enabled = true;
    }

    public void LookUnderBed()
    {
        player.SetActive(false);
        bedCam.gameObject.SetActive(true);

        Invoke(nameof(RotateCamera), 2.5f);
    }




    private void RotateCamera()
    {
        Vector3 initialRotation = bedCam.transform.rotation.eulerAngles;

        Debug.Log(rotationAngle);

        bedCam.transform.DORotate(new Vector3(0f, initialRotation.y + rotationAngle, 0f), rotationDuration / 4).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            bedCam.transform.DORotate(initialRotation, rotationDuration / 4).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                bedCam.transform.DORotate(new Vector3(0f, initialRotation.y - rotationAngle, 0f), rotationDuration / 4).SetEase(Ease.InOutQuad).OnComplete(() =>
                {
                    bedCam.transform.DORotate(initialRotation, rotationDuration / 4).SetEase(Ease.InOutQuad).OnComplete(TurnOffBedCam);
                });
            });
        });
    }

    private void TurnOffBedCam()
    {
        bedCam.gameObject.SetActive(false);
        player.SetActive(true);

    }
}