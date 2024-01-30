using UnityEngine;
using DG.Tweening;

public class CameraRotate : MonoBehaviour
{
    public float rotationSpeed = 30f; //    VIDEO 4 : rotationSpeed = 30f;

    void Start()
    {
        // Llama a la funci�n para iniciar la rotaci�n
        RotateCamera();
    }

    void RotateCamera()
    {
        // Obtiene la rotaci�n actual de la c�mara
        Vector3 currentRotation = transform.eulerAngles;

        // Calcula la rotaci�n final sum�ndole 90 grados al eje Y
        Vector3 targetRotation = new Vector3(currentRotation.x, currentRotation.y + 90f, currentRotation.z);

        // Utiliza Dotween para animar la rotaci�n hacia la rotaci�n final
        transform.DORotate(targetRotation, 90f / rotationSpeed)
            .SetEase(Ease.Linear);  // Configura la curva de interpolaci�n para una rotaci�n lineal
    }
}
