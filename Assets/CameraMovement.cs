using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{
    public Transform[] waypoints;  // Puntos de la ruta
    public float speed = 5f;       // Velocidad de movimiento

    private int currentWaypoint = 0;

    void Start()
    {
        // Llama a la funci�n MoveToWaypoint() al inicio para iniciar el movimiento
        MoveToWaypoint();
    }

    void MoveToWaypoint()
    {
        // Verifica si hay puntos de la ruta disponibles
        if (currentWaypoint < waypoints.Length)
        {
            // Calcula la duraci�n del tween basada en la velocidad y Time.deltaTime
            float duration = Vector3.Distance(transform.position, waypoints[currentWaypoint].position) / (speed * Time.deltaTime);

            // Utiliza Dotween para mover la c�mara al siguiente punto de la ruta
            transform.DOMove(waypoints[currentWaypoint].position, duration)
                .OnComplete(MoveToNextWaypoint);

            // Utiliza LookAt para que la c�mara mire hacia el pr�ximo punto
            transform.DOLookAt(waypoints[currentWaypoint].position, 1.0f);
        }
    }

    void MoveToNextWaypoint()
    {
        // Cambia al siguiente punto de la ruta
        currentWaypoint++;

        // Llama a la funci�n MoveToWaypoint() para continuar el movimiento
        MoveToWaypoint();
    }
}

