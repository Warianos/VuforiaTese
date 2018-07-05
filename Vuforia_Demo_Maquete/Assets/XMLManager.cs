using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using System.Collections.Generic; //lets use lists
using System.Xml;                 //basic xml attributes     
using System.Xml.Serialization;   //access xmlSerializer
using System.IO;                  //file management
using UnityEngine.SceneManagement;

public class XMLManager : MonoBehaviour {
    
    ///////////////////////////////////////INI Singleton pattern/////////////////////////
     
    public static XMLManager ins;
    private static bool created = false;


    // Use this for initialization

    private void Awake()
    {
        
    }

    private void Start()
    {
        ins = this;
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        LoadItems();
    }
    ///////////////////////////////////////FIM Singleton pattern/////////////////////////

    //list of items
    public ItemDatabase itemDB;

    //save functions
    public void SaveItems()
    {
        //open a new xmlFile
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        FileStream stream = new FileStream(Application.persistentDataPath + "/StreamingAssets/XML/item_data.xml", FileMode.Create);
        serializer.Serialize(stream, itemDB);
        stream.Close();

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif

    }
    //load function
    public void LoadItems()
    {
        //open a new xmlFile
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        FileStream stream = new FileStream(Application.persistentDataPath + "/StreamingAssets/XML/item_data.xml", FileMode.Open);
        itemDB = serializer.Deserialize(stream) as ItemDatabase;
        stream.Close();
    }

    public void setVariableInDatabase(string objectName,bool win)
    {

        foreach(MuseumObject item in itemDB.list)
        {
            if (item.objectName == objectName)
            {
                if (win != false)
                {
                    item.earnedSticker = win;
                }
            }
            
        }
        
    }
    public void setVariableInDatabase(string objectName, bool win, string objectInfoText1)
    {

        foreach (MuseumObject item in itemDB.list)
        {
            if (item.objectName == objectName)
            {
                if (objectInfoText1 != null)
                {
                    item.objectInfoText1 = objectInfoText1;
                }
                if (win != false)
                {
                    item.earnedSticker = win;
                }
            }

        }

    }
    public void setVariableInDatabase(string objectName, bool win, string objectInfoText1, string objectInfoText2)
    {

        foreach (MuseumObject item in itemDB.list)
        {
            if (item.objectName == objectName)
            {
                if (objectInfoText1 != null)
                {
                    item.objectInfoText1 = objectInfoText1;
                }
                if (objectInfoText2 != null)
                {
                    item.objectInfoText2 = objectInfoText2;
                }
                if (win != false)
                {
                    item.earnedSticker = win;
                }
            }

        }

    }
    public void setVariableInDatabase(string objectName, bool win, string objectInfoText1, string objectInfoText2, string objectInfoText3)
    {

        foreach (MuseumObject item in itemDB.list)
        {
            if (item.objectName == objectName)
            {
                if (objectInfoText1 != null)
                {
                    item.objectInfoText1 = objectInfoText1;
                }
                if (objectInfoText2 != null)
                {
                    item.objectInfoText2 = objectInfoText2;
                }
                if (objectInfoText3 != null)
                {
                    item.objectInfoText2 = objectInfoText3;
                }
                if (win != false)
                {
                    item.earnedSticker = win;
                }
            }

        }

    }


    public void LoadScene(string sceneName) // salvar as alterações para o xml antes de fazer load da scene e depois vais load do xml para que a próxima cena tenha as coisas actualizadas
    {
        //SaveItems();
        LoadItems();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetSceneByName(sceneName));
    }
    public void sceneManager()
    {
        if (SceneManager.GetActiveScene().name == "Telephone")
        {
            Screen.orientation = ScreenOrientation.Landscape;
        }
    }

}

[System.Serializable]
public class MuseumObject
{
    public string objectName;
    public bool earnedSticker;
    public string objectInfoText1;
    public string objectInfoText2;
    public string objectInfoText3;

   

}

[System.Serializable]
public class ItemDatabase
{
    [XmlArray("Objects")]
    public List<MuseumObject> list = new List<MuseumObject>();
}

// tenho de usar Application.persistentDataPath algures