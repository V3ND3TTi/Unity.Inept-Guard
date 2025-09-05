using UnityEngine;
using System.IO;
using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

public class DataManager : MonoBehaviour, IManager
{
    private string _dataPath;
    private string _textFile;
    private string _streamingTextFile;
    private string _xmlLevelProgress;
    private string _xmlWeapons;
    private string _jsonWeapons;

    private List<Weapon> weaponInventory = new List<Weapon>
    {
        new Weapon("Sword of Doom", 100),
        new Weapon("Butterfly Knives", 25),
        new Weapon("Brass Knuckles", 15),
    };

    private string _state;
    public string State
    {
        get => _state;
        set => _state = value;
    }

    void Awake()
    {
        _dataPath = $"{Application.persistentDataPath}/Player_Data/";
        _textFile = $"{_dataPath}Save_Data.txt";
        _streamingTextFile = $"{_dataPath}Streaming_Save_Data.txt";
        _xmlLevelProgress = $"{_dataPath}Progress_Data.xml";
        _xmlWeapons = $"{_dataPath}WeaponInventory.xml";
        _jsonWeapons = $"{_dataPath}WeaponJSON.json";
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _state = "Data Manager initialized...";
        Debug.Log(_state);

        // Text File Saves
        //FilesystemInfo();
        //NewDirectory();
        //NewTextFile();
        //UpdateTextFile();
        //ReadFromFile(_textFile);
        //DeleteFile(_textFile);

        // Streaming File Saves
        //WriteTostream(_streamingTextFile);
        //ReadFromStream(_streamingTextFile);

        // XML Files Saves
        //WriteToXML(_xmlLevelProgress);
        //ReadFromStream(_xmlLevelProgress); // We can also use ReadFromStream or ReadFromFile for XML

        // Serialize / Deserialize XML
        //SerializeXML();
        //DeserializeXML();

        // Serialize / Deserialize JSON
        //SerializeJSON();
        //DeserializeJSON();
    }

    public void FilesystemInfo()
    {
        Debug.LogFormat($"Path separator character: {Path.PathSeparator}");
        Debug.LogFormat($"Directory separator character: {Path.DirectorySeparatorChar}");
        Debug.LogFormat($"Current directory: {Directory.GetCurrentDirectory()}");
        Debug.LogFormat($"Temporary path: {Path.GetTempPath()}");
    }

    public void NewDirectory()
    {
        if (Directory.Exists(_dataPath))
        {
            Debug.Log("Directory already exists...");
            return;
        }

        Directory.CreateDirectory(_dataPath);
        Debug.Log("New directory created!");
    }

    public void DeleteDirectory()
    {
        if (!Directory.Exists(_dataPath))
        {
            Debug.Log("Directory doesn't exist...");
            return;
        }

        Directory.Delete(_dataPath, true);
        Debug.Log("Directory successfully deleted!");
    }

    public void NewTextFile()
    {
        if (File.Exists(_textFile))
        {
            Debug.Log("File already exists...");
            return;
        }

        File.WriteAllText(_textFile, "<SAVE DATA>\n");
        Debug.Log("New file created!");
    }

    public void UpdateTextFile()
    {
        if (!File.Exists(_textFile))
        {
            Debug.Log("File doesn't exist...");
            return;
        }

        File.AppendAllText(_textFile, $"Game started:{DateTime.Now}\n");
        Debug.Log("File updated successfully!");
    }

    public void ReadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File doesn't exist...");
            return;
        }

        Debug.Log(File.ReadAllText(filename));
    }

    public void DeleteFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File doesn't exist...");
            return;
        }

        File.Delete(_textFile);
        Debug.Log("File successfully deleted!");
    }

    public void WriteTostream(string filename)
    {
        if (!File.Exists(filename))
        {
            StreamWriter newStream = File.CreateText(filename);
            newStream.WriteLine("<Save Data> for HERO BORN\n");
            newStream.Close();
            Debug.Log("New file created with StreamWriter!");
        }

        StreamWriter streamWriter = File.AppendText(filename);
        streamWriter.WriteLine($"Game ended: {DateTime.Now}");
        streamWriter.Close();
        Debug.Log("File contents updated with StreamWriter!");
    }

    public void ReadFromStream(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File doesn't exist...");
            return;
        }

        StreamReader streamReader = new StreamReader(filename);
        Debug.Log(streamReader.ReadToEnd());
    }

    public void WriteToXML(string filename)
    {
        if (!File.Exists(filename))
        {
            FileStream xmlStream = File.Create(filename);
            XmlWriter xmlWriter = XmlWriter.Create(xmlStream);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("level_progress");

            for (int i = 1; i < 5; i++)
            {
                xmlWriter.WriteElementString("level", $"Level-{i}");
            }

            xmlWriter.WriteEndElement();
            xmlWriter.Close();
            xmlStream.Close();
        }
    }

    public void SerializeXML()
    {
        var xmlSerializer = new XmlSerializer(typeof(List<Weapon>));
        using (FileStream stream = File.Create(_xmlWeapons))
        {
            xmlSerializer.Serialize(stream, weaponInventory);
        }
    }

    public void DeserializeXML()
    {
        if (File.Exists(_xmlWeapons))
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Weapon>));

            using (FileStream stream = File.OpenRead(_xmlWeapons))
            {
                var weapons = (List<Weapon>)xmlSerializer.Deserialize(stream);

                foreach (var weapon in weapons)
                {
                    Debug.LogFormat($"Weapon: {weapon.Name} - Damage: {weapon.Damage}");
                }
            }
        }
    }

    public void SerializeJSON()
    {
        var shop = new WeaponShop();
        shop.inventory = weaponInventory;

        string jsonString = JsonUtility.ToJson(shop, true);

        using (StreamWriter stream = File.CreateText(_jsonWeapons))
        {
            stream.WriteLine(jsonString);
        }
    }

    public void DeserializeJSON()
    {
        if (File.Exists(_jsonWeapons))
        {
            using (StreamReader stream = new StreamReader(_jsonWeapons))
            {
                var jsonString = stream.ReadToEnd();
                var weaponData = JsonUtility.FromJson<WeaponShop>(jsonString);

                foreach (var weapon in weaponData.inventory)
                {
                    Debug.LogFormat($"Weapon: {weapon.Name} - Damage: {weapon.Damage}");
                }
            }
        }
    }
}
