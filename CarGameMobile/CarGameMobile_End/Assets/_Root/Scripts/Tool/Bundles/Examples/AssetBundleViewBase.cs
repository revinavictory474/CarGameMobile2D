using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Tool.Bundles.Examples
{
   internal class AssetBundleViewBase : MonoBehaviour
    {
        private const string UrlAssetBundleSprites = 
            "https://drive.google.com/uc?export=download&id=1__uv2Y7tdX2DLf8rVepoOGBo_zRchHuE";
        private const string UrlAssetBundleAudio = 
            "https://drive.google.com/uc?export=download&id=1zuZSl7CrtjQ33qbyrw7RpsynzQEzNRjP";
        private const string UrlBackgroundBundleSprites = 
            "https://drive.google.com/uc?export=download&id=13ub8N605l_QoySUq8E-DUzwJ_aTR_jaY";
               
        [SerializeField] private DataSpriteBundle[] _dataSpriteBundles;
        [SerializeField] private DataSpriteBundle[] _dataSpriteBackgroundBundles;
        [SerializeField] private DataAudioBundle[] _dataAudioBundles;

        private AssetBundle _spritesAssetBundle;
        private AssetBundle _audioAssetBundle;


        protected IEnumerator DownloadAndSetAssetBundles()
        {
            yield return GetSpritesAssetBundle(UrlAssetBundleSprites);
            yield return GetAudioAssetBundle();

            if (_spritesAssetBundle != null)
                SetSpriteAssets(_spritesAssetBundle, _dataSpriteBundles);
            else
                Debug.LogError($"AssetBundle {nameof(_spritesAssetBundle)} failed to load");

            if (_audioAssetBundle != null)
                SetAudioAssets(_audioAssetBundle);
            else
                Debug.LogError($"AssetBundle {nameof(_audioAssetBundle)} failed to load");
        }

        protected IEnumerator DownloadAndSetBackgroundButton()
        {
            yield return GetSpritesAssetBundle(UrlBackgroundBundleSprites);
            
            if(_spritesAssetBundle != null)
                SetSpriteAssets(_spritesAssetBundle, _dataSpriteBackgroundBundles);
            else 
                Debug.LogError($"AssetBundle {nameof(_spritesAssetBundle)} failed to load");
        }

        private IEnumerator GetSpritesAssetBundle(string url)
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url);

            yield return request.SendWebRequest();

            while (!request.isDone)
                yield return null;

            StateRequest(request, out _spritesAssetBundle);
        }

        private IEnumerator GetAudioAssetBundle()
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(UrlAssetBundleAudio);

            yield return request.SendWebRequest();

            while (!request.isDone)
                yield return null;

            StateRequest(request, out _audioAssetBundle);
        }

        private void StateRequest(UnityWebRequest request, out AssetBundle assetBundle)
        {
            if (request.error == null)
            {
                assetBundle = DownloadHandlerAssetBundle.GetContent(request);
                Debug.Log("Complete");
            }
            else
            {
                assetBundle = null;
                Debug.LogError(request.error);
            }
        }

        private void SetSpriteAssets(AssetBundle assetBundle, DataSpriteBundle[] dataSpriteBundle)
        {
            foreach (DataSpriteBundle data in dataSpriteBundle)
                data.Image.sprite = assetBundle.LoadAsset<Sprite>(data.NameAssetBundle);
        }

        private void SetAudioAssets(AssetBundle assetBundle)
        {
            foreach (DataAudioBundle data in _dataAudioBundles)
            {
                data.AudioSource.clip = assetBundle.LoadAsset<AudioClip>(data.NameAssetBundle);
                data.AudioSource.Play();
            }
        }
    }
}