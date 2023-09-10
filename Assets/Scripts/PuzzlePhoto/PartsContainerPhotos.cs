using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsContainerPhotos : MonoBehaviour
{
    [SerializeField] private PhotoPart[] photosParts;
    [SerializeField] private Transform[] correctPositonPhotoParts;

    [SerializeField] InventorySprites inventory;
    [SerializeField] GameObject completePhoto;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Se llama por unity event desde un pickable Item para informar que fue recolectado
    public void CheckActivePhotos(PickableItem pickableItem)
    {
        if (pickableItem.GetItemType() == PickeableItemType.photo)
        {
            for (int i = 0; i < photosParts.Length; i++)
            {
                if (photosParts[i].PhotoName == pickableItem.GetName())
                {
                    photosParts[i].IsVisualized = true;
                }
            }
        }
    }

    public void CheckWinCondition()
    {
        bool allPartsCorrect = true;

        for (int i = 0; i < photosParts.Length; i++)
        {
            if (photosParts[i].transform.position != correctPositonPhotoParts[i].position)
            {
                allPartsCorrect = false;
                break;
            }
        }

        if (allPartsCorrect)
        {
            for (int i = 0; i < photosParts.Length; i++)
            {
                photosParts[i].gameObject.SetActive(false);
            }

            completePhoto.SetActive(true);
            audioSource.Play();
        }
    }

    public bool CheckPhotoInCorrectPivot(PhotoPart correctPhotoPart)
    {
        int indexPhoto = correctPhotoPart.IDPart;

        if (Vector3.Distance(correctPhotoPart.transform.position, correctPositonPhotoParts[indexPhoto].position) < 0.02f)
        {
            correctPhotoPart.transform.position = correctPositonPhotoParts[indexPhoto].position;
            CheckWinCondition();
            return true;
        }

        return false;
    }
}