using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField]
    private PotionList potList;

    private readonly string jsonName = "/TestJS.json";
    private readonly string filePath = Application.dataPath +
        "/StreamingAssets/JSON-Data";
    private string dataPath;
    public TMPro.TMP_Text test_ui;

    private void Start()
    {
        dataPath = filePath + jsonName;
        //PrintURIData();
        //StartCoroutine(PrintReadJSONData());
        if (File.Exists(dataPath))
        {
            //LoadFromJSON();
        }
        //SaveIntoJSON("Sleep Potion", 100, "Sleep",
        //    "Apply sleep to target for 1 turn");
        //ReadFromJSON();
        //UpdateValue("Sleep Potion", 50);
        //DeleteData("Sleep Potion");
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

        //List<PotionData> listData = LoadFromJSON();
        //test_ui.text = "";
        //foreach (PotionData pd in listData)
        //{
        //    test_ui.text += "Potion Name: " + pd.potion_name + "\n";
        //    test_ui.text += "Potion Value: " + pd.value + "\n";
        //    test_ui.text += "Potion Effects: \n";
        //    foreach (Effect ev in pd.effect)
        //    {
        //        test_ui.text += "Effect Name: " + ev.name + "\n";
        //        test_ui.text += "Effect Desc: " + ev.desc + "\n\n";
        //    }
        //}

        StartCoroutine(PrintReadJSONData());
    }



    public PotionList LoadFromJSON()
    {
        Debug.Log($"Is file exist: {File.Exists(dataPath)}");
        if (File.Exists(dataPath))
        {
            string content = File.ReadAllText(dataPath);
            JsonUtility.FromJsonOverwrite(content, potList);
            foreach(PotionData pot in potList.list)
            {
                Debug.Log($"Raw JSON Data Name: {pot.potion_name}");
                Debug.Log($"Raw JSON Data Value: {pot.value}");
                Debug.Log($"Raw JSON Data Effect: {pot.effect}");
                Debug.Log($"Raw JSON Data Effect Name: {pot.effect[0].name}");
                Debug.Log($"Raw JSON Data Effect Desc: {pot.effect[0].desc}");
            }
            
            return potList;
        }
        else
        {
            return potList;
        }
    }

    public void SaveIntoJSON()
    {
        string potion = JsonUtility.ToJson(potList, true);
        File.WriteAllText(dataPath, potion);
    }

    public void SaveIntoJSON(string pName, int pValue,
        string eName, string eDesc)
    {
        PotionData pData = new();
        Effect pEffect = new();
        pEffect.name = eName;
        pEffect.desc = eDesc;
        pData.potion_name = pName;
        pData.value = pValue;
        pData.effect.Add(pEffect);
        potList.list.Add(pData);
        string potion = JsonUtility.ToJson(potList, true);
        File.WriteAllText(dataPath, potion);
        potList.list.Clear();
    }

    public void PrintURIData()
    {
        Debug.Log($"File data path: {dataPath}\n");
        Uri uri1 = new(dataPath);
        Debug.Log($"Testing URI\n");
        Debug.Log($"AbsolutePath: {uri1.AbsolutePath}\n");
        Debug.Log($"AbsoluteUri: {uri1.AbsoluteUri}\n");
        Debug.Log($"Authority: {uri1.Authority}\n");
        Debug.Log($"DnsSafeHost: {uri1.DnsSafeHost}\n");
        Debug.Log($"Fragment: {uri1.Fragment}\n");
        Debug.Log($"Host: {uri1.Host}\n");
        Debug.Log($"HostNameType: {uri1.HostNameType}\n");
        Debug.Log($"IdnHost: {uri1.IdnHost}\n");
        Debug.Log($"IsAbsoluteUri: {uri1.IsAbsoluteUri}\n");
        Debug.Log($"IsDefaultPort: {uri1.IsDefaultPort}\n");
        Debug.Log($"IsFile: {uri1.IsFile}\n");
        Debug.Log($"IsLoopback: {uri1.IsLoopback}\n");
        Debug.Log($"IsUnc: {uri1.IsUnc}\n");
        Debug.Log($"LocalPath: {uri1.LocalPath}\n");
        Debug.Log($"OriginalString: {uri1.OriginalString}\n");
        Debug.Log($"PathAndQuery: {uri1.PathAndQuery}\n");
        Debug.Log($"Port: {uri1.Port}\n");
        Debug.Log($"Query: {uri1.Query}\n");
        Debug.Log($"Scheme: {uri1.Scheme}\n");
        Debug.Log($"Segments: {string.Join(", ", uri1.Segments)}\n");
        Debug.Log($"UserEscaped: {uri1.UserEscaped}\n");
        Debug.Log($"UserInfo: {uri1.UserInfo}\n");
    }

    public void UpdateValue(string pName, int uValue)
    {
        foreach (PotionData pd in potList.list)
        {
            if (pd.potion_name.Equals(pName))
            {
                pd.value = uValue;
                break;
            }
        }
        SaveIntoJSON();
    }

    public void DeleteData(string pName)
    {
        foreach (PotionData pd in potList.list)
        {
            if (pd.potion_name.Equals(pName))
            {
                potList.list.Remove(pd);
                break;
            }
        }
        SaveIntoJSON();
    }

    IEnumerator PrintReadJSONData()
    {
        yield return new WaitForSeconds(3f);

        potList = LoadFromJSON();
        test_ui.text = "";
        foreach (PotionData pd in potList.list)
        {
            test_ui.text += "Potion Name: " + pd.potion_name + "\n";
            test_ui.text += "Potion Value: " + pd.value + "\n";
            test_ui.text += "Potion Effects: \n";
            foreach (Effect ev in pd.effect)
            {
                test_ui.text += "Effect Name: " + ev.name + "\n";
                test_ui.text += "Effect Desc: " + ev.desc + "\n\n";
            }
        }

        StopCoroutine(PrintReadJSONData());
    }
}

[Serializable]
public class PotionList
{
    public List<PotionData> list = new();
}

[Serializable]
public class PotionData
{
    public string potion_name;
    public int value;
    public List<Effect> effect = new();
}

[Serializable]
public class Effect
{
    public string name;
    public string desc;
}