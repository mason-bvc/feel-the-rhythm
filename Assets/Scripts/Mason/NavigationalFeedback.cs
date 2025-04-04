using UnityEngine;
using UnityEngine.InputSystem;

public class NavigationalFeedback : MonoBehaviour
{
    private CharacterController characterController;
    public float Frequency = 1f;

    public void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // public void Start()
    // {
    //     if (Gamepad.current != null)
    //     {
    //         StartCoroutine("DoVibrationTest");
    //     }
    // }

    // public IEnumerator DoVibrationTest()
    // {
    //     // TODO: change rhyhtm depending on how close or far you are to orb
    //     Gamepad.current.SetMotorSpeeds(0.25f, 0.75f);
    //     yield return new WaitForSeconds(0.125f);
    //     Gamepad.current.SetMotorSpeeds(0, 0);
    //     yield return new WaitForSeconds(0.125f);
    //     StartCoroutine("DoVibrationTest");
    // }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
        Vector3 v = new(1, 0, 1);

        v.Scale(characterController.velocity);
        Frequency = v.sqrMagnitude / 10f;

        if (Mathf.Abs(Frequency) < 0.01f)
        {
            Gamepad.current?.SetMotorSpeeds(0, 0);
        }
        else
        {
            float t = Time.time * Frequency;
            Gamepad.current?.SetMotorSpeeds(0, Mathf.Sign(Mathf.Sin(2f * Mathf.PI * t)));
        }
    }
}
