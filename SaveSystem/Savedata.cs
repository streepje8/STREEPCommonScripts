using Openverse.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


/*
 * Savedata class
 * streep
 * 30/03/2022
 * 
 * Singleton based savedata class
 */
public class Savedata : Singleton<Savedata>
{
    private Dictionary<string, object> saveData = new Dictionary<string, object>();
    public string filePath = SaveFile;

    public static string SaveFile;
    public GameEvent dataLoadedEvent;

    private void Awake()
    {
        SaveFile =
#if UNITY_EDITOR
            Application.dataPath + "/savefile.dat";
#else
        Application.persistentDataPath + "/savefile.dat";
#endif
        filePath = SaveFile;
        Instance = this;
        DontDestroyOnLoad(this);
        loadAll();
    }

    private void OnApplicationQuit()
    {
        saveAll();
    }


    public void saveAll()
    {
        filePath = SaveFile;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(filePath, FileMode.OpenOrCreate);
        bf.Serialize(file, saveData);
        file.Close();
        file.Dispose();
    }

    private bool ready = false;
    private void Update()
    {
        if(ready)
        {
            dataLoadedEvent?.Raise();
            ready = false;
        }
    }

    public void loadAll()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            saveData = (Dictionary<string,object>)bf.Deserialize(file);
            file.Close();
            file.Dispose();
        } else
        {
            saveAll();
        }
        ready = true;
    }

    public void save(string path, System.Object data)
    {
        if (data.GetType().IsSerializable)
        {
            if (saveData.ContainsKey(path))
            {
                saveData.Remove(path);
            }
            saveData.Add(path,data);
        } else
        {
            Debug.LogError("You tried to save a piece of data that is not serializeable!");
        }
    }

    public System.Object get(string path)
    {
        if (saveData.ContainsKey(path))
        {
            return saveData[path];
        }
        else
        {
            return null;
        }
    }

    public int getInt(string path)
    {
        if (saveData.ContainsKey(path))
        {
            if (saveData[path].GetType() == typeof(int))
            {
                return (int)saveData[path];
            } else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }

    public bool getBool(string path)
    {
        if (saveData.ContainsKey(path))
        {
            if (saveData[path].GetType() == typeof(bool))
            {
                return (bool)saveData[path];
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public float getFloat(string path)
    {
        if (saveData.ContainsKey(path))
        {
            if (saveData[path].GetType() == typeof(float))
            {
                return (float)saveData[path];
            }
            else
            {
                return 0f;
            }
        }
        else
        {
            return 0f;
        }
    }

    public string getString(string path)
    {
        if (saveData.ContainsKey(path))
        {
            if (saveData[path].GetType() == typeof(string))
            {
                return (string)saveData[path];
            }
            else
            {
                return "null";
            }
        }
        else
        {
            return "null";
        }
    }

    public int getInt(string path, int defaultValue)
    {
        if (saveData.ContainsKey(path))
        {
            if (saveData[path].GetType() == typeof(int))
            {
                return (int)saveData[path];
            }
            else
            {
                return defaultValue;
            }
        }
        else
        {
            return defaultValue;
        }
    }

    public float getFloat(string path, float defaultValue)
    {
        if (saveData.ContainsKey(path))
        {
            if (saveData[path].GetType() == typeof(float))
            {
                return (float)saveData[path];
            }
            else
            {
                return defaultValue;
            }
        }
        else
        {
            return defaultValue;
        }
    }

    public string getString(string path, string defaultValue)
    {
        if (saveData.ContainsKey(path))
        {
            if (saveData[path].GetType() == typeof(string))
            {
                return (string)saveData[path];
            }
            else
            {
                return defaultValue;
            }
        }
        else
        {
            return defaultValue;
        }
    }
}
