using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField]
    private PotionData _PotionData =
        new();

    private readonly string jsonName = "/TestJS.json";
    private readonly string filePath = Application.dataPath +
        "/StreamingAssets/JSON-Data";
    private string dataPath;
    public TMPro.TMP_Text test_ui;

    private void Start()
    {
        dataPath = filePath + jsonName;
    }

    public void ReadFromJSON()
    {
        //PotionData loaded = new();
        //string rawData = File.ReadAllText(dataPath);
        //loaded = JsonUtility.FromJson<PotionData>(rawData);
        //test_ui.text = "";
        //test_ui.text += "Potion Name: " + loaded.potion_name + "\n";
        //test_ui.text += "Potion Value: " + loaded.value + "\n";
        //test_ui.text += "Potion Effects: \n";
        //Effect[] effectData = loaded.effect.ToArray();
        //test_ui.text += "Effect Name: " + effectData[0].name + "\n";
        //test_ui.text += "Effect Desc: " + effectData[0].desc + "\n";
        //test_ui.text += "\n";
        List<PotionData> listData = new();
        string rawData = File.ReadAllText(dataPath);
        listData = JsonUtility.FromJson<List<PotionData>>(dataPath);
        test_ui.text = "";
        foreach (PotionData pd in listData)
        {
            test_ui.text = "Potion Name: " + pd.potion_name + "\n";
            test_ui.text = "Potion Value: " + pd.value + "\n";
            test_ui.text = "Potion Effects: \n";
            foreach (Effect ev in pd.effect)
            {
                test_ui.text = "Effect Name: " + ev.name + "\n";
                test_ui.text = "Effect Desc: " + ev.desc + "\n\n";
            }
        }
    }

    public List<PotionData> LoadFromJSON()
    {
        if (File.Exists(dataPath))
        {
            string content = File.ReadAllText(dataPath);
            List<PotionData> rawData = JsonUtility.FromJson<List<PotionData>>(content);
            return rawData;
        }
        else
        {
            return new List<PotionData>();
        }
    }

    public void SaveIntoJSON()
    {
        string potion = JsonUtility.ToJson(_PotionData);
        File.WriteAllText(dataPath, potion);
    }
}

[System.Serializable]
public class PotionData
{
    public string potion_name;
    public int value;
    public List<Effect> effect = new();
}

[System.Serializable]
public class Effect
{
    public string name;
    public string desc;
}