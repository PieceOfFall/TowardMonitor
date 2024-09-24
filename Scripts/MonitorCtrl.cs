using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class MonitorCtrl : MonoBehaviour
{
    public string UrlsFileName;
    public List<VLCPlayerExample> PlayerExamples;

    void Start()
    {
        ReadLiveURL();
    }

    async void ReadLiveURL()
    {
        string text = await ReadFileFromStreamingAssetsAsync(UrlsFileName);
        List<string> urls = JsonConvert.DeserializeObject<UrlsVo>(text).urls;
        for (int i = 0; i < PlayerExamples.Count; i++)
        {
            PlayerExamples[i].OpenUrl(urls[i]);
        }
    }

    public async Task<string> ReadFileFromStreamingAssetsAsync(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            return await Task.Run(() => File.ReadAllText(filePath));
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
            return null;
        }
    }
}
