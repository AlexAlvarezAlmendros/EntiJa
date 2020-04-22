using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class FileCheck : MonoBehaviour
{
    public int? savedScore;
    public int savedRecord;

    void Start()
    {
        //LoadFile();
        //Debug.Log("Loaded");
        //if (savedScore < GameController.instance.hiscore)
        //{
        //    Debug.Log("saved is lower");
        //    GameController.instance.record = GameController.instance.hiscore;
        //    SaveFile();
        //}
    }

    public void LoadFile()
    {
        string destination = "save.sav";

        if (File.Exists(destination))
        {
            FileStream file = new FileStream(destination, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            savedScore = formatter.Deserialize(file) as int?;
            if (savedScore == null)
            {
                Debug.LogError("Error charging score, null read");
                savedScore = 0;
            }
            file.Close();
        }
        else
        {
            Debug.LogError("File not found");
            GameController.instance.record = GameController.instance.hiscore;
            SaveFile();
        }
    }

    public void SaveFile()
    {
        string destination = "save.sav";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = new FileStream(destination, FileMode.Create);

        formatter.Serialize(file, GameController.instance.hiscore);
        file.Close();
    }
}