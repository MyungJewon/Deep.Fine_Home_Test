using UnityEngine;
using System; // 이벤트 정의를 위해 추가
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
public class LoaderModule : MonoBehaviour
{
    ObjImporter objImporter= new ObjImporter();         //구조체 선언
    public event Action<GameObject> OnLoadCompleted;    //이벤트 선언

    public void LoadAsset(string assetName)             //동기 시스템 
    {
        List<Verticesmap> loadedObject = objImporter.LoadObj(assetName);    //obj파일 읽기
        string gameobjectname=Path.GetFileNameWithoutExtension(assetName);
        //메쉬 적용 함수
        OnLoadCompleted?.Invoke(Addmeshtoarrayfromobj(loadedObject,gameobjectname));                       //이벤트 반환
    }
    public async Task<GameObject> LoadAssetAsync(string assetName)
    {
        //obj파일을 읽고 데이터만 저장
        List<Verticesmap> loadedObject =await Task.Run(() => objImporter.LoadObj(assetName));
        //gameobject 이름설정
        string gameobjectname=Path.GetFileNameWithoutExtension(assetName);
        Debug.Log(gameobjectname+" 시작");
        //메쉬 적용 함수
        return Addmeshtoarrayfromobj(loadedObject,gameobjectname); 
    }

    private GameObject Addmeshtoarrayfromobj(List<Verticesmap> loadedObject,string gameobjectname){
        //task에서 실행이 불가능한 Unity Api를 메인 threads에서 진행
        //여러 그룹으로 이루어진 obj일경우
            if(loadedObject.Count>1){
                GameObject objParent = new GameObject(gameobjectname);
                for(int i=0;i<loadedObject.Count;i++){
                    Mesh mesh = new Mesh();
                    //버텍스는 공유하기 때문에 마지막 버텍스 배열만 사용
                    mesh.vertices = loadedObject[loadedObject.Count-1].vertices.ToArray();
                    mesh.triangles = loadedObject[i].triangles.ToArray();
                    mesh.RecalculateNormals(); // 법선 재계산

                    // 메쉬 필터 컴포넌트에 메쉬 적용
                    GameObject tempobj = new GameObject(loadedObject[i].objname);
                    MeshFilter meshFilter = tempobj.AddComponent<MeshFilter>();
                    meshFilter.mesh = mesh;

                    // MeshRenderer를 추가하여 Mesh를 렌더링합니다.
                    MeshRenderer meshRenderer = tempobj.AddComponent<MeshRenderer>();
                    meshRenderer.material = new Material(Shader.Find("Standard"));
                    tempobj.transform.SetParent(objParent.transform);
                }
                Debug.Log(gameobjectname+" 끝");
                return objParent;
            }
            //그룹이 없는 오브젝트일 경우
            else{
                Mesh mesh = new Mesh();
                mesh.vertices = loadedObject[0].vertices.ToArray();
                mesh.triangles = loadedObject[0].triangles.ToArray();
                mesh.RecalculateNormals(); // 법선 재계산

                // 메쉬 필터 컴포넌트에 메쉬 적용
                GameObject tempobj = new GameObject(gameobjectname);
                MeshFilter meshFilter = tempobj.AddComponent<MeshFilter>();
                meshFilter.mesh = mesh;

                // MeshRenderer를 추가하여 Mesh를 렌더링합니다.
                MeshRenderer meshRenderer = tempobj.AddComponent<MeshRenderer>();
                meshRenderer.material = new Material(Shader.Find("Standard"));
                Debug.Log(gameobjectname+" 끝");
                return tempobj;
        }
    }
}
