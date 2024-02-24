using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
public class Verticesmap    //데이터를 쉽게 옮길 클래스 선언
{      
    public List<Vector3> vertices = new List<Vector3>();
    public List<Vector2> uv = new List<Vector2>();
    public List<int> triangles = new List<int>();
    public string groupname = "groupname";
    public Verticesmap(string groupname, List<Vector3> vertices, List<Vector2> uv, List<int> triangles)
    {
        this.groupname = groupname; this.vertices = vertices; this.uv = uv; this.triangles = triangles;
    }
    public Verticesmap(string groupname, List<Vector2> uv, List<int> triangles)
    {
        this.groupname = groupname; this.uv = uv; this.triangles = triangles;
    }
}
public class ObjImporter : MonoBehaviour
{
    public List<Verticesmap> LoadObj(string path){
        bool objendcheck = false;
        string groupname = "objnametemp";
        List<Verticesmap> objectList = new List<Verticesmap>(); // 오브젝트 리스트 생성
        string[] lines = File.ReadAllLines(path);   //파일경로로 읽기
        //데이터 저장용 변수 선언
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();
        List<int> triangles = new List<int>();

        foreach (var line in lines)     //모든 데이터를 읽을때까지
        {
            //복합 오브젝트인경우 g으로 나뉨
            if (line.StartsWith("g "))
            {
                //여러 그룹이 있을경우 그룹의 시작과 끝을 알기위한 if
                if (objendcheck == true)
                {
                    //vertices는 공유함으로 groupname, uv, triangles만 저장 vertices는 비워둠
                    Verticesmap oneverticesmap = new Verticesmap(groupname, uv, triangles);
                    objectList.Add(oneverticesmap);
                    //새로운 그룹을 저장하기위해 초기화
                    uv = new List<Vector2>();
                    triangles = new List<int>();
                    //그룹 종료 알림
                    objendcheck = false;
                    //새로운그룹명 저장
                    groupname = line;
                }
                else
                {
                    //g 부분삭제
                    groupname = line.Substring(2);
                    objendcheck = true;
                }
            }
            else if (line.StartsWith("v ")) // 정점 데이터
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
                var faceData = faceDataString.Trim().Split(' ');
                // mesh 확인 사각형이라면 삼각형 mesh로 변경
                if (faceData.Length > 3)
                {
                    faceData = new string[] { faceData[0], faceData[1], faceData[2], faceData[0], faceData[2], faceData[3] };
                }
                foreach (var vertex in faceData)
                {
                    var vertexInfo = vertex.Split('/');
                    // OBJ 인덱스는 1부터 시작, Unity 인덱스는 0부터 시작
                    triangles.Add(int.Parse(vertexInfo[0]) - 1);
                }
            }
        }
        //마지막그룹 or 하나의 그룹일경우를 위한 데이터 저장, 이때는 vertices를 포함한다.
        Verticesmap lastverticesmap = new Verticesmap(groupname, vertices, uv, triangles);
        objectList.Add(lastverticesmap);
        return objectList;
    }
}