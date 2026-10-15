using System;
using UnityEngine;

public class CatapultInput : MonoBehaviour
{
    [SerializeField] private KeyCode _launchKey = KeyCode.F;
    [SerializeField] private KeyCode _reloadKey = KeyCode.R;

    public event Action LaunchPressed;
    public event Action ReloadPressed;

    private void Update()
    {
        if (Input.GetKeyDown(_launchKey))
            LaunchPressed?.Invoke();

        if (Input.GetKeyDown(_reloadKey))
            ReloadPressed?.Invoke();
    }
}