using System;
using UnityEngine;

public class SwingInput : MonoBehaviour
{
    [SerializeField] private KeyCode _swingKey = KeyCode.G;

    public event Action SwingPressed;

    private void Update()
    {
        if (Input.GetKeyDown(_swingKey))
            SwingPressed?.Invoke();
    }
}