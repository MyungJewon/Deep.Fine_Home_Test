# Deep.Fine_Home_Test

## 개요

1. .obj파일의 내부 정점과, 메쉬를 읽고 유니티로 가져오는 코드
2. 동기, 비동기 시스템구현

## 특징

1. 복합 .obj파일을 그대로 import 가능

![그림1](https://github.com/MyungJewon/Deep.Fine_Home_Test/assets/54784949/8325ff4a-2e8f-4b97-9ed5-32afeea56d12)
좌) 구현된시스템, 우) Unity로 import한 obj

1. AssetLoaderAsyncMulti.cs 의 경우 복수의 .obj를 읽기 유용하게 하기 위해 파일이 아닌 폴더를 읽도록 함

```csharp
public string[] LoadFiles(string directoryPath) //Directory에서 fileExtension 확장자만 저장
    {
        string[] files = Directory.GetFiles(directoryPath, "*" + fileExtension)
                                  .Where(path => Path.GetExtension(path).Equals(fileExtension))
                                  .ToArray();
        return files;
    }
```

## 코드 설명

### AssetLoader.cs

1. void Load(assetName): 경로를 받아 이벤트를 추가하고 LoaderModule를 실행 시키는 함수
2. void OnLoadCompleted(loadedAsset): 이벤트를 사용한  gameobject처리

### AssetLoaderAsync.cs

1. Task Load(assetNames): 경로를 받고 LoaderModule를 실행시키는 함수, gameobject처리

### AssetLoaderAsyncMulti.cs

1. string[] LoadFiles(directoryPath): 폴더에서 .obj파일을 읽고 배열화 하는 함수 
2. Task Load(assetNames[]): 모든 작업을 Task화 하고 실행시키는 함수

### LoaderModule.cs

1. void LoadAsset(path): 동기 방식에서 사용되는 함수
2. Task<GameObject> LoadAssetAsync(path): 비동기 방식에서 사용되는 함수
3. GameObject Addmeshtoarrayfromobj(loadedObject , gameobjectname) : 계산된 vertices와 mesh를 가지고 GameObject를 만드는 함수

### ObjImporter.cs

1. List<Verticesmap> LoadObj(path): LoaderModule에서 호출되는 함수, .obj를 읽고 vertices와 mesh, group을 추출하는 함
2. class Verticesmap: 그룹별 데이터(mesh, 그룹명) 저장편의를 위해 생성된 class선언

### issus
1. 특정한 .obj의 경우 삼각형 mesh가 아닌 사각형 mesh를 사용, 사각형일경우 삼각형으로 쪼개는 과정을 포함
![그림2](https://github.com/MyungJewon/Deep.Fine_Home_Test/assets/54784949/2c11eaa6-acd2-4e6e-8e5d-dd40e17966a7)
```csharp
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
```
