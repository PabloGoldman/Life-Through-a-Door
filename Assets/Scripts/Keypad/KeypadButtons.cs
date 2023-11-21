using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadButtons : MonoBehaviour
{
    [SerializeField] uint number;
    [SerializeField] bool isEnter = false;

    PassCode passCode;
    private Tween currentTween; // Almacena la referencia al tween actual


    private void Start()
    {
        passCode = GetComponentInParent<PassCode>();
    }

    private void OnMouseUpAsButton()
    {
        if (currentTween != null && currentTween.IsActive())
        {
            // Si hay un tween en curso, finalizarlo inmediatamente
            currentTween.Complete();
        }

        if (isEnter)
        {
            passCode.Enter();
        }
        else
        {
            passCode.CodeFuntion(number.ToString());

            // Ajusta los par�metros para un movimiento m�s leve hacia atr�s
            currentTween = transform.DOPunchPosition(new Vector3(0, 0, -0.05f), 0.3f, vibrato: 5, elasticity: 1)
                .OnComplete(() => ResetPosition());
        }
    }

    // M�todo para restablecer la posici�n original
    private void ResetPosition()
    {
        currentTween = transform.DOPunchPosition(Vector3.zero, 0.2f);
    }
}
