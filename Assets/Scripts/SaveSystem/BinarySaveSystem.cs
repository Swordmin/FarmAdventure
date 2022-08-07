using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinarySaveSystem : MonoBehaviour
{
    private readonly string _filePath;

    public BinarySaveSystem()
    {
        _filePath = Application.persistentDataPath + "/Save.dat";
    }

    public void Save(SaveData data)
    {
        using (FileStream file = File.Create(_filePath))
        {
            new BinaryFormatter().Serialize(file, data);
        }
    }

    public SaveData Load()
    {
        SaveData saveData;

        using (FileStream file = File.Open(_filePath, FileMode.Open))
        {
            object loadedData = new BinaryFormatter().Deserialize(file);
            saveData = (SaveData)loadedData;
        }

        return saveData;
    }

    public bool IsFileExist()
    {
        if (File.Exists(Application.persistentDataPath + "/Save.dat"))
            return true;
        return false;
    }
}

[Serializable]
public class SaveData
{
    public List<Level> _levels;
}

public class Level 
{

    public string Name;
    public int Stars;
    public bool IsFinal;

}
