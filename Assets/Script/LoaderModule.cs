using UnityEngine;
using System; // 이벤트 정의를 위해 추가
using Dummiesman;
using UnityEditor.PackageManager;

public class LoaderModule : MonoBehaviour
{
    public event Action<GameObject> OnLoadCompleted; 

    public void LoadAsset(string assetName)
    {
        GameObject loadedAsset = new OBJLoader().Load(assetName);
        OnLoadCompleted?.Invoke(loadedAsset);
    }
}