
using UnityEngine;
using System.Collections;

public class Curserlock : MonoBehaviour
{

    public bool lockCursor = true;

    void Update()
    {

        // pressing esc toggles between hide/show
        if (Input.GetKeyDown(KeyCode.Escape))
        {
                lockCursor = !lockCursor;
        }


        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockCursor;
    }
}