using UnityEngine;
using UnityEngine.UI;

public class LissajousMovement : MonoBehaviour
{
    public Toggle animateToggle;
    public Transform objectToAnimate;

    private float time; //Current time for animation
    private float timeMultiplier = 2f; //Speed of the animation
    private float amplitudeMultiplier = 1f; //Amplitude of the movement
    //Amplitude for the x and y oscillations
    private float amplitudeX;
    private float amplitudeY;
    //Frequencies of the oscillations for the x and y
    private float frequencyX;
    private float frequencyY;
    private float phaseDelay; //Phase delay between x and y oscillations
    private Vector3 objectOriginalPosition;
    private bool wasToggleOn;


    void Start()
    {
        SetAnimationParameters();
        objectOriginalPosition = objectToAnimate.position;
    }

    private void SetAnimationParameters()
    {
        amplitudeX = Random.Range(1f, 3f);
        amplitudeY = Random.Range(1f, 3f);
        frequencyX = Random.Range(1f, 3f);
        frequencyY = Random.Range(1f, 3f);
        phaseDelay = Random.Range(0f, 1f);
        time = 0;
    }


    void Update()
    {
        if (animateToggle.isOn)
        {
            time = Time.time * timeMultiplier;
            //Calculate the new position and move the object to it
            Vector3 finalPos = transform.position;
            finalPos.x = amplitudeX * Mathf.Sin((frequencyX * time) + phaseDelay) * amplitudeMultiplier;
            finalPos.y = amplitudeY * Mathf.Sin(frequencyY * time) * amplitudeMultiplier;
            transform.position = finalPos;
        }
        else if (wasToggleOn)
        {
            objectToAnimate.position = objectOriginalPosition;
            SetAnimationParameters();
        }
        wasToggleOn = animateToggle.isOn;
    }
}