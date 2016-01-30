using UnityEngine;
using System.Collections;

public class ShadowProjector : MonoBehaviour {

    public Transform directionalLight;
    public GameObject shadowPlane;

    public Transform shadowCastingVertex0;
    public Transform shadowCastingVertex1;
    public Transform shadowCastingVertex2;
    public Transform shadowCastingVertex3;

    public int[] mapping;

    private Plane castToPlane;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private Mesh mesh;

    private Vector3[] vertices;

    void Start()
    {
        castToPlane = new Plane(-Vector3.forward, Vector3.zero);

        meshRenderer = shadowPlane.GetComponent<MeshRenderer>();
        meshFilter = meshRenderer.GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;
        meshFilter.mesh = mesh;
        vertices = mesh.vertices;
    }

    void Update()
    {
        // For each vertex, raycast the plane and place the vertex
        Raycast(shadowCastingVertex0, mapping[0]);
        Raycast(shadowCastingVertex1, mapping[1]);
        // Raycast(shadowCastingVertex2, mapping[2]);
        // Raycast(shadowCastingVertex3, mapping[3]);

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }

    void Raycast(Transform rayOrigin, int vertexIndex)
    {
        float distance;
        Ray ray = new Ray(rayOrigin.position, directionalLight.forward);
        
        if (castToPlane.Raycast(ray, out distance))
        {
            Debug.DrawRay(ray.origin, ray.direction * distance);
            vertices[vertexIndex] = transform.InverseTransformPoint(ray.origin + ray.direction * distance);
        }
    }

}
