using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
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
        string selectedAssetName = EditorUtility.OpenFolderPanel("Select obj model", "", "obj");
        string[] directoryPath = LoadFiles(selectedAssetName);
        await Load(directoryPath);
    }

    public async Task Load(string[] assetNames)
    {
        int movepoint=0;
        var tasks = new List<Task<GameObject>>();
        for (int i = 0; i < assetNames.Length; i++)
        {
            tasks.Add(loaderModuleasyncmulti.LoadAssetAsync(assetNames[i]));
        }

        while (tasks.Count > 0)
        {
            var loadedAssets = await Task.WhenAny(tasks);
            GameObject loadedAsset = await loadedAssets;
            loadedAsset.transform.SetParent(transform);
            loadedAsset.transform.Translate(Vector3.forward  *movepoint);
            movepoint+=200;
            tasks.Remove(loadedAssets);
        }
        Debug.Log("작업끝");
    }

    public string[] LoadFiles(string directoryPath)
    {
        string[] files = Directory.GetFiles(directoryPath, "*" + fileExtension)
                                  .Where(path => Path.GetExtension(path).Equals(fileExtension))
                                  .ToArray();
        return files;
    }
}
