using UnityEngine;
using System; // 이벤트 정의를 위해 추가
using System.Threading.Tasks;
using System.Collections.Generic;
public class LoaderModule : MonoBehaviour
{
    ObjImporter objImporter= new ObjImporter();         //구조체 선언
    public event Action<GameObject> OnLoadCompleted;    //이벤트 선언

    public void LoadAsset(string assetName)             //동기 시스템 
    {
        GameObject loadedAsset = objImporter.LoadObj(assetName);    //obj파일 읽기
        OnLoadCompleted?.Invoke(loadedAsset);                       //이벤트 반환
    }
    public async Task<GameObject> LoadAssetAsync(string assetName)
    {
        Verticesmap loadedObject =await Task.Run(() => objImporter.LoadObjAsync(assetName));    //obj파일을 읽고 데이터만 저장

        //task에서 실행이 불가능한 Unity Api를 메인 threads에서 진행
        Mesh mesh = new Mesh();
        mesh.vertices = loadedObject.vertices.ToArray();
        mesh.uv = loadedObject.uv.ToArray(); //텍스처 불필요
        mesh.triangles = loadedObject.triangles.ToArray();
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