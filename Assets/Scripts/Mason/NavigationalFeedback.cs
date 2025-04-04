using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class NavigationalFeedback : MonoBehaviour
{
    public void Start()
    {
        if (Gamepad.current != null)
        {
            StartCoroutine("DoVibrationTest");
        }
    }

    public IEnumerator DoVibrationTest()
    {
        // TODO: change rhyhtm depending on how close or far you are to orb
        Gamepad.current.SetMotorSpeeds(0.25f, 0.75f);
        yield return new WaitForSeconds(0.125f);
        Gamepad.current.SetMotorSpeeds(0, 0);
        yield return new WaitForSeconds(0.125f);
        StartCoroutine("DoVibrationTest");
    }
}
