using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;

public class CinematicManager : MonoBehaviourSingleton<CinematicManager>
{
    Camera cam;
    AudioManager am;

    [SerializeField] GameObject player;

    [SerializeField] FPSCameraController fpsCamera;
    [SerializeField] FPSController fpsController;


    [Header("Hidden Puzzle transitions")]
    [SerializeField] CinemachineVirtualCamera bedCam;

    [SerializeField] float rotationAngle;
    [SerializeField] float rotationDuration;

    [Header("Hidden Puzzle transitions")]
    [SerializeField] CinemachineVirtualCamera playerCam;
    [SerializeField] Camera visualCam;
    [SerializeField] Transform entryDoor;
    [SerializeField] PostProcessVolume postProcessGO;
    ColorGrading colorGradient;

    private void Start()
    {
        cam = Camera.main;
        am = AudioManager.Get();

        colorGradient = postProcessGO.profile.GetSetting<ColorGrading>();
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
        am.PlayBendSound();

        Invoke(nameof(RotateCamera), 2.5f);
    }

    private void RotateCamera()
    {
        Vector3 initialRotation = bedCam.transform.rotation.eulerAngles;

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
        am.PlayGetUpSound();
        player.SetActive(true);

    }

    public void LookAtEntryDoor()
    {
        postProcessGO.gameObject.SetActive(true);
        colorGradient.colorFilter.value = Color.white;

        StartCoroutine(FinalSecondStage());
      
    }

    IEnumerator FinalSecondStage()
    {
        float secondsBetweenTransitions = 0.5f;
        float lookAtAnimationDuration = 2.0f;
        float colorGradientDuration = 1.0f;


        FreezePlayer();

        yield return new WaitForSeconds(secondsBetweenTransitions);

        visualCam.gameObject.SetActive(false);


        playerCam.transform.DOLookAt(entryDoor.position, lookAtAnimationDuration + secondsBetweenTransitions);

        yield return new WaitForSeconds(lookAtAnimationDuration + secondsBetweenTransitions);

        DOTween.To(() => colorGradient.colorFilter.value, x => colorGradient.colorFilter.value = x, Color.black, colorGradientDuration)
        .OnComplete(() => Debug.Log("Mando los sonidos"));



        yield return new WaitForSeconds(5.5f);


        SceneManager.Get().LoadScene("ThirdPuzzle");
    }    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            LookAtEntryDoor();
        }
    }
}
