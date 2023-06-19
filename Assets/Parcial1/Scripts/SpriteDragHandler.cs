using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpriteDragHandler : MonoBehaviour
{
    [SerializeField] Camera secondCamera;
    public UnityEvent<string> SnappedPhoto;

    private Vector3 screenPointOffset;
    private Vector3 initialPosition;
    PhotoPart photoPart;
    PartsContainerPhotos partsContainer;
    private void Awake()
    {
        partsContainer = transform.GetComponentInParent<PartsContainerPhotos>();
        photoPart = transform.GetComponent<PhotoPart>();
        
    }
    private void OnMouseDown()
    {
        // Obtener la posici�n inicial del objeto en coordenadas del mundo
        initialPosition = transform.position;

        // Calcular la diferencia entre la posici�n del objeto y la posici�n del mouse en pantalla
        screenPointOffset = transform.position - secondCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        // Obtener la posici�n actual del mouse en pantalla y convertirla a coordenadas del mundo
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPointOffset.z);
        Vector3 currentPosition = secondCamera.ScreenToWorldPoint(currentScreenPoint + screenPointOffset);

        // Convertir la posici�n actual del objeto a coordenadas de viewport
        Vector3 viewportPosition = secondCamera.WorldToViewportPoint(currentPosition);

        // Limitar la posici�n del objeto dentro del viewport (entre 0 y 1)
        viewportPosition.x = Mathf.Clamp01(viewportPosition.x);
        viewportPosition.y = Mathf.Clamp01(viewportPosition.y);

        // Convertir la posici�n del viewport a coordenadas del mundo
        currentPosition = secondCamera.ViewportToWorldPoint(viewportPosition);

        // Actualizar la posici�n del objeto
        transform.position = new Vector3(currentPosition.x, currentPosition.y, transform.position.z);
    }

    private void OnMouseUp()
    {

        if (partsContainer.CheckPhotoInCorrectPivot(photoPart))
        {
            SnappedPhoto.Invoke(photoPart.PhotoName);
            Destroy(this);
        }
        else
        {
            //Depende de lo que queramos lo podemos volver a poner en su posicion inicial
            transform.position = initialPosition;
        }
    }


}
