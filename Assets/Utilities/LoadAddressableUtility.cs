using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
#endif
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Main.utility
{
    public class LoadAddressableUtility : MonoBehaviour
    {
        private readonly Dictionary<string, object> loadedAddresables = new();

        public async UniTask<T> LoadAddressable<T>(string name, CancellationToken cancellationToken)
        {
            if (loadedAddresables.ContainsKey(name))
                return (T)loadedAddresables[name];
            
            var handle = Addressables.LoadAssetAsync<T>(name);
            await handle.Task;
            if (handle.Result != null && !loadedAddresables.ContainsKey(name))
                loadedAddresables.Add(name, handle.Result);
            return handle.Result;
        }

        public async Task<SceneInstance> LoadScene(string addressableName)
        {
            var sceneInstance = await Addressables.LoadSceneAsync(addressableName, LoadSceneMode.Additive);
            return sceneInstance;
        }

        public async UniTaskVoid UnloadScene(SceneInstance instance)
        {
            await Addressables.UnloadSceneAsync(instance);
        }

#if UNITY_EDITOR     
        public static void AddAssetToGroup(string assetPath ,string groupName)
        {
            var settings = AddressableAssetSettingsDefaultObject.Settings;
            
            var group = AddressableAssetSettingsDefaultObject.Settings.FindGroup(groupName);
            if (!group)
            {
                group = settings.CreateGroup(groupName, false, false, true, null, typeof(ContentUpdateGroupSchema), typeof(BundledAssetGroupSchema));
            }
            
            var entry = AddressableAssetSettingsDefaultObject.Settings.CreateOrMoveEntry(AssetDatabase.AssetPathToGUID(assetPath), group);
 
            if (entry == null)
            {
                throw new Exception($"Addressable : can't add {assetPath} to group {groupName}");
            }
        }

        public static void RemoveAddressableAsset(string assetPath)
        {
            AssetDatabase.DeleteAsset(assetPath);
        }
#endif
    }
}