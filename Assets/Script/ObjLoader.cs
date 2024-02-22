using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ObjImporter : MonoBehaviour
{
    public GameObject LoadObj(string path)
    {
        // 파일에서 모든 라인을 읽음
        string[] lines = File.ReadAllLines(path);

        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();
        List<int> triangles = new List<int>();

        foreach (var line in lines)
        {
            if (line.StartsWith("v ")) // 정점 데이터
            {
                var vertexData = line.Substring(2).Split(' ');
                vertices.Add(new Vector3(
                    float.Parse(vertexData[0]),
                    float.Parse(vertexData[1]),
                    float.Parse(vertexData[2])));
            }
            else if (line.StartsWith("vt ")) // 텍스처 좌표 데이터
            {
                var uvData = line.Substring(3).Split(' ');
                uv.Add(new Vector2(
                    float.Parse(uvData[0]),
                    float.Parse(uvData[1])));
            }
            else if (line.StartsWith("f ")) // 면 데이터
            {
                var faceData = line.Substring(2).Split(' ');
                foreach (var vertex in faceData)
                {
                    var vertexInfo = vertex.Split('/');
                    // OBJ 인덱스는 1부터 시작하지만, Unity 인덱스는 0부터 시작합니다.
                    triangles.Add(int.Parse(vertexInfo[0]) - 1);
                }
            }
        }
        // 메쉬 생성 및 적용
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.uv = uv.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals(); // 법선 재계산

        // 메쉬 필터 컴포넌트에 메쉬 적용
        GameObject obj = new GameObject("Object");
        MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        // MeshRenderer를 추가하여 Mesh를 렌더링합니다.
        MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard")); // 적절한 Material을 설정합니다.

        return obj;
    }
}