using UnityEngine;
using UnityEditor;

public class MeshManager : MonoBehaviour
{
    private MeshFilter meshFilter;

    public int latCount = 20; //Horizontal lines count
    public int lonCount = 20; //Vertical lines count
    public float radius = 1f;       

    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        ProceduralSphere();
    }

    public void ProceduralSphere()
    {
        Mesh mesh = new Mesh();

        //The total vertices and triangles calculation is based on latitude and longitude lines
        Vector3[] vertices = new Vector3[(latCount + 1) * (lonCount + 1)];
        int[] triangles = new int[latCount * lonCount * 6];

        int vertexIndex = 0;
        //Generating vertices for the sphere
        for (int lat = 0; lat <= latCount; lat++)
        {
            //Lerping the y pos between -radius and +radius
            float y = Mathf.Lerp(-radius, radius, (float)lat / latCount); 
            float currentRadius = Mathf.Sqrt(radius * radius - y * y);

            for (int lon = 0; lon <= lonCount; lon++)
            {
                //Calculating the angle around the latitude line
                float angle = (float)lon / lonCount * Mathf.PI * 2;
                //Converting from polar coord to cartesian coord, as its accepted by Unity
                float x = currentRadius * Mathf.Cos(angle); 
                float z = currentRadius * Mathf.Sin(angle); 
                vertices[vertexIndex] = new Vector3(x, y, z);
                vertexIndex++;
            }
        }

        int triangleIndex = 0;
        //Generating the triangles to connect the vertices to a mesh
        for (int lat = 0; lat < latCount; lat++)
        {
            for (int lon = 0; lon < lonCount; lon++)
            {
                int current = lat * (lonCount + 1) + lon;
                int next = current + lonCount + 1;
                //First half of the triangle
                triangles[triangleIndex++] = current;
                triangles[triangleIndex++] = next;
                triangles[triangleIndex++] = current + 1;
                //Second half of the triangle
                triangles[triangleIndex++] = current + 1;
                triangles[triangleIndex++] = next;
                triangles[triangleIndex++] = next + 1;
            }
        }  

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals(); //Needed to reflect lighting
        meshFilter.mesh = mesh;
    }
}
