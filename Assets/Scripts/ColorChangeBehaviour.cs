using UnityEngine;
using UnityEngine.UI;

public class ColorChangeBehaviour : MonoBehaviour
{
    public Toggle animateToggle;
    public Transform targetObject;

    private Material[] childrenMaterials;
    private float timer;
    private Vector3 objectOriginalPosition;
    private bool wasToggleOn;

    void Start()
    {
        childrenMaterials = new Material[transform.childCount];
        objectOriginalPosition = targetObject.transform.localPosition;
        //Store material for each child
        for (int i = 0; i < transform.childCount; i++)
        {
            childrenMaterials[i] = transform.GetChild(i).GetComponent<MeshRenderer>().material;
        }

    }

    void Update()
    {
        if (animateToggle.isOn)
        {
            timer += Time.deltaTime;
            float x = Mathf.Cos(timer) * 3; //X axis circular offset
            float z = Mathf.Sin(timer) * 3; //Z axis circular offset
            targetObject.localPosition = transform.localPosition + new Vector3(x, 0, z);

            Vector3 direction = (targetObject.localPosition - transform.localPosition).normalized;
            //The dot product to measure alignment between forward and direction vectors
            float dot = Vector3.Dot(transform.forward, direction);

            foreach (var material in childrenMaterials)
            {
                //Normalise dot product to range 0 to 1
                float blendFactor = (dot + 1) / 2;

                //Interpolate colour between blue and red
                material.color = Color.Lerp(Color.blue, Color.red, blendFactor);
            }

        }
        else if (wasToggleOn)
        {
            //Reset everything
            targetObject.transform.localPosition = objectOriginalPosition;
            timer = 0;
            foreach (var material in childrenMaterials)
            {
                material.color = Color.red;
            }
        }
        wasToggleOn = animateToggle.isOn;
    }
}
