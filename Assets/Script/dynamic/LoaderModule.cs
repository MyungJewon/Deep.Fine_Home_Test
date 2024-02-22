using UnityEngine;
using System; // 이벤트 정의를 위해 추가
using UnityEditor.PackageManager;

public class LoaderModule : MonoBehaviour
{
    ObjImporter objImporter= new ObjImporter();
    public event Action<GameObject> OnLoadCompleted; 

    public void LoadAsset(string assetName)
    {
         GameObject loadedAsset = objImporter.LoadObj(assetName);
         OnLoadCompleted?.Invoke(loadedAsset);      
    }
}