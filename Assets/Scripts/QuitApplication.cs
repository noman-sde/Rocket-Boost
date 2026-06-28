using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.escapeKey.IsPressed())
        {
            Debug.Log("Escape key pressed. Quitting application.");
            Application.Quit();
        }
    }
}
