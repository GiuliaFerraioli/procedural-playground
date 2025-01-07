using UnityEngine;
using UnityEngine.UI;

public class RotationMovement : MonoBehaviour
{
    public Toggle animateToggle;
    public Transform targetObject;
    public float rotationSpeed = 200f;
    public float moveSpeed = 5f;

    private Vector3[] initialChildrenPositions;
    private Quaternion[] initialChildrenRotations;
    private bool wasToggleOn;

    void Start()
    {
        //Store initial rotation and position of each child
        initialChildrenPositions = new Vector3[transform.childCount];
        initialChildrenRotations = new Quaternion[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            initialChildrenPositions[i] = transform.GetChild(i).position;
            initialChildrenRotations[i] = transform.GetChild(i).rotation;
        }
    }

    void Update()
    {
        if (animateToggle.isOn)
        {
            foreach (Transform child in transform)
            {
                //Keep the movement on the X axis ignoring Y and Z axes
                Vector3 direction = targetObject.position - child.position;
                direction.y = 0;
                direction.z = 0;
                //Distance between the child and the target obj
                float distance = direction.magnitude;

                if (distance > 0.1f)
                {
                    //We normalize to ensure the speed remain constant
                    direction.Normalize();
                    //Move and rotate until the target is reached
                    child.position += direction * moveSpeed * Time.deltaTime;
                    child.Rotate(Vector3.up * Time.deltaTime * rotationSpeed, Space.Self);
                }
            }
        }
        else if (wasToggleOn)
        {
            //Reset each child to its initial position
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                child.position = initialChildrenPositions[i];
                child.rotation = initialChildrenRotations[i];
            }
        }
        wasToggleOn = animateToggle.isOn;
    }
}
