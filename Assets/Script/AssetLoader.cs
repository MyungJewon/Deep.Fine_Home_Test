<<<<<<< HEAD
using UnityEngine;
using UnityEditor;

=======
// 위 AssetLoader class에서 LoaderModule.LoadAsset(assetName)을 호출해 3d model을 로딩하는 코드가 있습니다.
// 이 코드를 Unity GameObject에 Component로 추가 후
// LoaderModule 내부 로직을 구현해 제시한 AssetLoader가 동작하도록 로직을 구현해 주세요.

// - 제약 조건: 모델 파일은 obj 파일이며 최종적으로 play scene에 obj 파일이 동적 로드된 형태로 보여야 함.
// - 프로젝트 환경 설정 방법: Unity project 형태로 생성해서 Unity editor에서 열어서 바로 실행할 수 있는 형태여야 함
// - 리뷰 포인트: LoaderModule을 구현한 코드에 대해 정확성 리뷰
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
>>>>>>> origin/main
public class AssetLoader : MonoBehaviour
{
    [SerializeField]
    private LoaderModule loaderModule;
<<<<<<< HEAD
=======
    public string directoryPath = "your/directory/path";
    public string fileExtension = ".obj";

    public string[] LoadFiles(string directoryPath)
    {
        // 해당 디렉토리에서 모든 파일을 가져옵니다.
        string[] files = Directory.GetFiles(directoryPath, "*" + fileExtension)
                                  .Where(path => Path.GetExtension(path).Equals(fileExtension))
                                  .ToArray();

        foreach (var file in files)
        {
            Debug.Log("Found file: " + file);
            // 파일을 처리하는 로직을 여기에 구현합니다.
        }
        return files;
    }
>>>>>>> origin/main

    private void Start()
    {
        loaderModule = new LoaderModule();
<<<<<<< HEAD
        string selectedAssetName = EditorUtility.OpenFilePanel("Select obj model", "", "obj");
        Load(selectedAssetName);
    }
    public void Load(string assetName)
    {
        loaderModule.OnLoadCompleted += OnLoadCompleted;
        loaderModule.LoadAsset(assetName);
    }
=======
        //string selectedAssetName = EditorUtility.OpenFilePanel("Select obj model", "", "obj");
        //Load(selectedAssetName);
        string selectedAssetName = EditorUtility.OpenFolderPanel("Select obj model", "", "obj");
        string[] files = LoadFiles(selectedAssetName);
        Load(files);
    }

    public void Load(string[] assetName)
    {
        loaderModule.OnLoadCompleted += OnLoadCompleted;
        for(int i = 0; i < assetName.Length; i++)
        {
            loaderModule.LoadAsset(assetName[i]);

        }

    }

>>>>>>> origin/main
    private void OnLoadCompleted(GameObject loadedAsset)
    {
        loadedAsset.transform.SetParent(transform);
        // To do
    }
}