using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private KeyCode _launchKey = KeyCode.F;
    [SerializeField] private KeyCode _reloadKey = KeyCode.R;
    [SerializeField] private KeyCode _swingKey = KeyCode.G;

    public event Action LaunchPressed;
    public event Action ReloadPressed;
    public event Action SwingPressed;

    private void Update()
    {
        if (Input.GetKeyDown(_launchKey))
            LaunchPressed?.Invoke();

        if (Input.GetKeyDown(_reloadKey))
            ReloadPressed?.Invoke();

        if (Input.GetKeyDown(_swingKey))
            SwingPressed?.Invoke();
    }
}