using UnityEngine;

public class MeshManager : MonoBehaviour
{
    private MeshFilter sphereMeshFilter;
    private MeshFilter coneMeshFilter;

    [Header("Objects material")]
    public Material goMaterial;
    [Header("Sphere parameters")] public bool showSphere = true;
    public int sphereLatCount = 20; //Horizontal lines count
    public int sphereLonCount = 20; //Vertical lines count
    public float sphereRadius = 1f;
    [Header("Cone parameters")]
    public bool showCone = true;
    public int coneSegments = 20;
    public float coneHeight = 0.5f;
    public float coneRadius = 0.3f;

    private void Awake()
    {
        if (showSphere)
        {
            SphereMeshSetup();
            ProceduralSphereGenerator();
        }

        if (showCone)
        {
            ConeMeshSetup();
            ProceduralConeGenerator();
        }

    }

    private void SphereMeshSetup()
    {
        GameObject sphereGo = new GameObject("Sphere");
        sphereGo.transform.SetParent(transform);
        sphereGo.transform.localPosition = Vector3.zero;

        sphereMeshFilter = sphereGo.AddComponent<MeshFilter>();
        MeshRenderer sphereRenderer = sphereGo.AddComponent<MeshRenderer>();
        sphereRenderer.material = goMaterial;
    }

    private void ConeMeshSetup()
    {
        GameObject coneGo = new GameObject("Cone");
        coneGo.transform.SetParent(transform);
        coneGo.transform.localPosition = new Vector3(0, 0, 2);

        coneMeshFilter = coneGo.AddComponent<MeshFilter>();
        MeshRenderer coneRenderer = coneGo.AddComponent<MeshRenderer>();
        coneRenderer.material = goMaterial;
    }

    //Generate a sphere mesh procedurally based on latitude, longitude and sphereRadius
    public void ProceduralSphereGenerator()
    {
        Mesh mesh = new Mesh();

        //Arrays to store vertex positions and triangle indices
        int vertexCount = (sphereLatCount + 1) * (sphereLonCount + 1);
        Vector3[] vertices = new Vector3[vertexCount];
        //Uv coordinates for texture
        Vector2[] uvs = new Vector2[vertexCount];

        int triangleCount = sphereLatCount * sphereLonCount * 6;
        int[] triangles = new int[triangleCount];

        int vertexIndex = 0;

        //Loop to generate vertices for the sphere
        for (int lat = 0; lat <= sphereLatCount; lat++)
        {
            //Interpolate the y pos between -sphereRadius and +sphereRadius
            float y = Mathf.Lerp(-sphereRadius, sphereRadius, (float)lat / sphereLatCount);
            //Radius of the circle at the current latitude
            float circleRadius = Mathf.Sqrt(sphereRadius * sphereRadius - y * y);
            float verticalCoord = (float)lat / sphereLatCount;


            for (int lon = 0; lon <= sphereLonCount; lon++)
            {
                //Calculate the angle around the circle for the current longitude
                float angle = (float)lon / sphereLonCount * Mathf.PI * 2;

                //Convert from polar coordinates to cartesian coordinates
                float x = circleRadius * Mathf.Cos(angle);
                float z = circleRadius * Mathf.Sin(angle);

                //Store vertex position
                vertices[vertexIndex] = new Vector3(x, y, z);
                uvs[vertexIndex] = new Vector2((float)lon / sphereLonCount, verticalCoord);

                vertexIndex++;
            }
        }

        int triangleIndex = 0;

        //Loop to generate triangles connecting the vertices
        for (int lat = 0; lat < sphereLatCount; lat++)
        {
            for (int lon = 0; lon < sphereLonCount; lon++)
            {
                int currentVertex = lat * (sphereLonCount + 1) + lon;
                int nextRowVertex = currentVertex + sphereLonCount + 1;

                //First triangle of the quad
                triangles[triangleIndex] = currentVertex;
                triangles[triangleIndex + 1] = nextRowVertex;
                triangles[triangleIndex + 2] = currentVertex + 1;

                //Second triangle of the quad   
                triangles[triangleIndex + 3] = currentVertex + 1;
                triangles[triangleIndex + 4] = nextRowVertex;
                triangles[triangleIndex + 5] = nextRowVertex + 1;

                triangleIndex += 6;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals(); //Calculate normals to reflect lighting
        sphereMeshFilter.mesh = mesh;
    }

    //Generate a cone mesh procedurally based on coneHeight, coneRadius, and coneSegments
    private void ProceduralConeGenerator()
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[coneSegments + 2];

        //Top of the cone
        vertices[0] = new Vector3(0, coneHeight, 0);

        //Loop to generate vertices around the cone base
        for (int i = 0; i < coneSegments; i++)
        {
            float angle = i * Mathf.PI * 2 / coneSegments;
            float x = Mathf.Cos(angle) * coneRadius;
            float z = Mathf.Sin(angle) * coneRadius;
            vertices[i + 1] = new Vector3(x, 0, z);
        }

        //Last vertex is the centre of the base
        vertices[coneSegments + 1] = Vector3.zero;

        //Trainlges for the cone side and base
        int[] triangles = new int[coneSegments * 6];
        int triangleIndex = 0;

        //Loop to generate side triangles
        for (int i = 0; i < coneSegments; i++)
        {
            triangles[triangleIndex] = 0; //Top
            triangles[triangleIndex + 1] = i + 1; //Base vertex
            triangles[triangleIndex + 2] = ((i + 1) % coneSegments) + 1; //Next base vertex

            triangleIndex += 3;
        }

        //Loop to generate base traingles
        for (int i = 0; i < coneSegments; i++)
        {
            triangles[triangleIndex] = coneSegments + 1; //Center of base
            triangles[triangleIndex + 1] = ((i + 1) % coneSegments) + 1; //Next base vertex
            triangles[triangleIndex + 2] = i + 1; //Current base vertex

            triangleIndex += 3;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        coneMeshFilter.mesh = mesh;
    }


}
