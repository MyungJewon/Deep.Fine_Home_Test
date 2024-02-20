using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ObjImporter : MonoBehaviour
{
    public Mesh ImportFile(string filePath)
    {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();
        List<int> triangles = new List<int>();

        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            string[] parts = line.Trim().Split(' ');

            switch (parts[0])
            {
                case "v":
                    // 정점 정보를 읽어옵니다.
                    float x = float.Parse(parts[1]);
                    float y = float.Parse(parts[2]);
                    float z = float.Parse(parts[3]);
                    vertices.Add(new Vector3(x, y, z));
                    break;
                case "vn":
                    // 법선 정보를 읽어옵니다.
                    float nx = float.Parse(parts[1]);
                    float ny = float.Parse(parts[2]);
                    float nz = float.Parse(parts[3]);
                    normals.Add(new Vector3(nx, ny, nz));
                    break;
                case "vt":
                    // UV 정보를 읽어옵니다.
                    float u = float.Parse(parts[1]);
                    float v = float.Parse(parts[2]);
                    uv.Add(new Vector2(u, v));
                    break;
                case "f":
                    // 면 정보를 읽어옵니다.
                    for (int i = 1; i < parts.Length; i++)
                    {
                        string[] indices = parts[i].Split('/');
                        int vertexIndex = int.Parse(indices[0]) - 1;
                        int uvIndex = int.Parse(indices[1]) - 1;
                        int normalIndex = int.Parse(indices[2]) - 1;
                        triangles.Add(vertexIndex);
                    }
                    break;
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.normals = normals.ToArray();
        mesh.uv = uv.ToArray();
        mesh.triangles = triangles.ToArray();

        return mesh;
    }
    void LoadObj(string filePath)
    {
        // .obj 파일을 읽어옵니다.
        ObjImporter objImporter = new ObjImporter();
        Mesh mesh = objImporter.ImportFile(filePath);

        // 읽어온 Mesh를 GameObject에 할당합니다.
        GameObject obj = new GameObject("LoadedObject");
        MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        // Mesh를 렌더링하기 위해 MeshRenderer를 추가합니다.
        MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard")); // 적절한 Material을 설정합니다.
    }
}