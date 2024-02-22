using UnityEngine;
using System; // 이벤트 정의를 위해 추가
using System.Threading.Tasks;
using System.Runtime.Serialization;

public class LoaderModule : MonoBehaviour
{
    ObjImporter objImporter= new ObjImporter();
    public event Action<GameObject> OnLoadCompleted; 

    public void LoadAsset(string assetName)
    {
        //GameObject loadedObject = new OBJLoader().Load(assetName);
        //OnLoadCompleted?.Invoke(loadedObject);
        GameObject loadedAsset = objImporter.LoadObj(assetName);
        OnLoadCompleted?.Invoke(loadedAsset);
    }
    public async Task<GameObject> LoadAssetAsync(string assetName)
    {
        //GameObject loadedObject = new OBJLoader().Load(assetName);
        Verticesmap loadedObject =await Task.Run(() => objImporter.LoadObjAsync(assetName));

        Mesh mesh = new Mesh();
        mesh.vertices = loadedObject.vertices.ToArray();
        mesh.uv = loadedObject.uv.ToArray();
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