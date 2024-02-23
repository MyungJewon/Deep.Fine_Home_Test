using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


public class AssetLoaderAsyncMulti : MonoBehaviour
{
    [field: SerializeField]
    private LoaderModule loaderModuleasyncmulti;
    public string fileExtension = ".obj";
    private async void Start()
    {
        loaderModuleasyncmulti = new LoaderModule();
        try{
        string selectedAssetName = EditorUtility.OpenFolderPanel("Select obj model", "", "obj");
        string[] directoryPath = LoadFiles(selectedAssetName);  //디랙토리의 파일 저장
        await Load(directoryPath);
        }catch(Exception ex){   //Data 예외처리
            Debug.Log("Data type(obj) error");
        }
    }

    public async Task Load(string[] assetNames)
    {
        int movepoint=0;
        var tasks = new List<Task<GameObject>>();
        for (int i = 0; i < assetNames.Length; i++)
        {
            tasks.Add(loaderModuleasyncmulti.LoadAssetAsync(assetNames[i]));    //모든 작업 task화
        }

        while (tasks.Count > 0)
        {
            var loadedAssets = await Task.WhenAny(tasks);   //task 반환하는 대로 나머지 작업수행
            GameObject loadedAsset = await loadedAssets;    //loadedAsset에 저장
            loadedAsset.transform.SetParent(transform);     //자식으로 설정
            loadedAsset.transform.Translate(Vector3.forward  *movepoint);   //겹치는걸 방지하기위한 transform변경
            movepoint+=200;
            tasks.Remove(loadedAssets); //완료된 Task 지우기
        }
        Debug.Log("작업끝");
    }

    public string[] LoadFiles(string directoryPath) //Directory에서 fileExtension 확장자만 저장
    {
        string[] files = Directory.GetFiles(directoryPath, "*" + fileExtension)
                                  .Where(path => Path.GetExtension(path).Equals(fileExtension))
                                  .ToArray();
        return files;
    }
}
