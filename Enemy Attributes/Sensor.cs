using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Sensor : MonoBehaviour {
    [SerializeField] float distance = 10f;
    [SerializeField] float angle = 30f;
    [SerializeField] float height = 1.0f;
    [SerializeField] Color meshColor = Color.red;
    [SerializeField] int scanFrequency = 30;
    [SerializeField] LayerMask layerMask;

    public List<GameObject> Objects {
        get {
            objects.RemoveAll(obj => !obj);
            return objects;
        }
    }
    List<GameObject> objects = new List<GameObject>();

    Collider[] colliders = new Collider[50];
    Mesh mesh;
    int count;
    float scanInterval;
    float scanTimer;
    
    void Start() {
        scanInterval = 1f / scanFrequency;
    }


    void OnValidate() {
        mesh = CreateWedgeMesh();
        scanInterval = 1f / scanFrequency;
    }

    void Update() {
        scanTimer -= Time.deltaTime;
        if (scanTimer < 0) {
            scanTimer += scanInterval;
            Scan();
        }
    }

    void OnDrawGizmos() {
        if (mesh) {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }

        Gizmos.DrawWireSphere(transform.position, distance);
        for (int i = 0; i < count; i++) {
            Gizmos.DrawSphere(colliders[i].transform.position, .2f);
        }

        Gizmos.color = Color.green;
        foreach (GameObject obj in Objects) {
            Gizmos.DrawSphere(obj.transform.position, .2f);            
        }
    }

    public bool IsInSight(GameObject obj) {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 dir = dest - origin;
        
        dir.y = 0f;
        float deltaAngle = Vector3.Angle(dir, transform.forward);
        if (deltaAngle > angle) {
            return false;
        }

        // if considering height
        // if (dir.y < 0 || dir.y > height) {
        //     return false;
        // }
        return true;
    }

    void Scan() {
        count = Physics.OverlapSphereNonAlloc(transform.position, distance, colliders, layerMask, QueryTriggerInteraction.Collide);

        objects.Clear();
        for (int i = 0; i < count; ++i) {
            GameObject obj = colliders[i].gameObject;
            if (IsInSight(obj)) {
                objects.Add(obj);
            }
        }
    }

    Mesh CreateWedgeMesh() {
        Mesh mesh = new Mesh();

        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;

        int vert = 0;

        // left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        // right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;

        for (int i = 0; i < segments; i++) {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance;

            topLeft = bottomLeft + Vector3.up * height;
            topRight = bottomRight + Vector3.up * height;

            // far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            // top
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            // bottom
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;
        }

        for (int i = 0; i < numVertices; ++i) {
            triangles[i] = i;
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}