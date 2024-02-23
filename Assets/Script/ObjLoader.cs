using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class Verticesmap {      //구조체 선언

    public List<Vector3> vertices = new List<Vector3>();
    public List<Vector2> uv = new List<Vector2>();
    public List<int> triangles = new List<int>();
    public Verticesmap(List<Vector3> vertices, List<Vector2> uv, List<int> triangles)
    {
        this.vertices = vertices; this.uv = uv; this.triangles = triangles;
    }
}
public class ObjImporter : MonoBehaviour
{
    public GameObject LoadObj(string path)
    {
        string[] lines = File.ReadAllLines(path);   //파일경로로 읽기

        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();
        List<int> triangles = new List<int>();

        foreach (var line in lines)     //모든 라인을 읽을때까지
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
                    // OBJ 인덱스는 1부터 시작, Unity 인덱스는 0부터 시작
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

    public Verticesmap LoadObjAsync(string path)
    {
        string[] lines = File.ReadAllLines(path);   //파일경로로 읽기
        List<Verticesmap> objectList = new List<Verticesmap>(); // 오브젝트 리스트 생성
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();
        List<int> triangles = new List<int>();

        foreach (var line in lines)     //모든 데이터를 읽을때까지
        {
                if (line.StartsWith("v ")) // 정점 데이터
                {
                    var vertexDataString = line.Substring(2);
                    var vertexData = vertexDataString.Trim().Split(' ');
                    vertices.Add(new Vector3(
                        float.Parse(vertexData[0]),
                        float.Parse(vertexData[1]),
                        float.Parse(vertexData[2])));
                }
                else if (line.StartsWith("vt ")) // 텍스처 좌표 데이터
                {
                    var uvDataStraing = line.Substring(3);
                    var uvData = uvDataStraing.Trim().Split(' ');
                    uv.Add(new Vector2(
                        float.Parse(uvData[0]),
                        float.Parse(uvData[1])));
                }
                else if (line.StartsWith("f ")) // 면 데이터
                {
                var faceDataString = line.Substring(2);
                var faceData =faceDataString.Trim().Split(' ');
                foreach (var vertex in faceData)
                    {
                        var vertexInfo = vertex.Split('/');
                        // OBJ 인덱스는 1부터 시작, Unity 인덱스는 0부터 시작
                        triangles.Add(int.Parse(vertexInfo[0]) -1 );
                    }
                }         
        }
        Verticesmap verticesmap= new Verticesmap(vertices,uv, triangles);
        return verticesmap ;
    }
}