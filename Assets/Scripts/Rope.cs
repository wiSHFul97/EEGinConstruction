using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rope : MonoBehaviour
{
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform endTransform;
    [SerializeField]float ropeSize = 0.1f;

    Vector3[] vertices;
    int[] triangles;
    Mesh mesh;

    private Vector3 startPoint;
    private Vector3 endPoint;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // List<Vector3> test  = new List<Vector3>();
        mesh = new Mesh();
        this.GetComponent<MeshFilter>().mesh = mesh;
        triangles = new int[24];
        setupMesh();
        updateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        setupMesh();
        updateMesh();
    }

    // IEnumerator changeTriangle()
    // {
    //     while (true)
    //     {
    //         for (int i = 0; i < vertices.Length; i++)
    //         {
    //             vertices[i].y = Random.Range(-2, 2);
    //             yield return new WaitForSeconds(0.5f);
    //         }
    //
    //         updateMesh();
    //     }
    // }

    private Vector3[] makeTheSquare()
    {
        Vector3[] res = new Vector3[8];
        startPoint =   startTransform.position -this.transform.position;
        endPoint =  endTransform.position - this.transform.position;
        res[0] = startPoint + startTransform.up * ropeSize + startTransform.right * ropeSize;
        res[1] = startPoint + startTransform.up * ropeSize - startTransform.right * ropeSize;
        res[2] = startPoint - startTransform.up * ropeSize - startTransform.right * ropeSize;
        res[3] = startPoint - startTransform.up * ropeSize + startTransform.right * ropeSize;
        
        res[4] = endPoint + startTransform.up * ropeSize + startTransform.right * ropeSize;
        res[5] = endPoint + startTransform.up * ropeSize - startTransform.right * ropeSize;
        res[6] = endPoint - startTransform.up * ropeSize - startTransform.right * ropeSize;
        res[7] = endPoint - startTransform.up * ropeSize + startTransform.right * ropeSize;

        return res;
    }


    // private void createShape()
    // {
    //     vertices = new Vector3[]
    //     {
    //         new Vector3(0, 0, 0),
    //         new Vector3(0, 0, 1),
    //         new Vector3(1, 0, 0)
    //     };
    //     triangles = new int[] {0, 1, 2};
    // }


    // private void createShape()
    // {
    //     vertices = new Vector3[]
    //     {
    //         new Vector3(0, 0, 0),
    //         new Vector3(0, 0, 1),
    //         new Vector3(1, 0, 0)
    //     };
    //     triangles = new int[] {0, 1, 2};
    // }


    private void setupMesh()
    {
        //setup vertices
        vertices = makeTheSquare();
        triangles = new int[36];
        //setup triangles 
        for (int index = 0; index < 3; index++)
        {
            triangles[0 + index * 6] = 1 + index;
            triangles[1 + index * 6] = 4 + index;
            triangles[2 + index * 6] = 0 + index;
            triangles[3 + index * 6] = 1 + index;
            triangles[4 + index * 6] = 5 + index;
            triangles[5 + index * 6] = 4 + index;
        }

        //end side
        triangles[18] = 3;
        triangles[19] = 0;
        triangles[20] = 4;
        triangles[21] = 4;
        triangles[22] = 7;
        triangles[23] = 3;
        
        //up
        triangles[24] = 0;
        triangles[25] = 3;
        triangles[26] = 1;
        triangles[27] = 3;
        triangles[28] = 2;
        triangles[29] = 1;
        
        //down 
        triangles[30] = 6;
        triangles[31] = 7;
        triangles[32] = 4;
        triangles[33] = 6;
        triangles[34] = 4;
        triangles[35] = 5;

    }

    private void updateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}