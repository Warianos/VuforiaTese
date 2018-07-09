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
        //SaveItems();
        Debug.Log("entrei aqui no start do XMLManager");
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            string objectName = "Telephone";
            bool win = true;
            string objectInfoText1 = "Antigamente, quando se queria telefonar a alguem, era necessário rodar a manivela ao lado das bobinas vermelhas, de maneira a produzir energia suficiente para enviar um impulso eletrico à central telefónica para podermos dizer a um/uma resposavel, o numero de telefone para o qual queriamos ligar, é por isso que este telefone não tem nenhum marcador de numeros.";
            string objectInfoText2 = "Antigamente, quando se queria telefonar a alguem, era necessário rodar a manivela ao lado das bobinas vermelhas, de maneira a produzir energia suficiente para enviar um impulso eletrico à central telefónica para podermos dizer a um/ uma resposavel, o numero de telefone para o qual queriamos ligar, é por isso que este telefone não tem nenhum marcador de numeros.";
            string objectInfoText3 = "Antigamente, quando se queria telefonar a alguem, era necessário rodar a manivela ao lado das bobinas vermelhas, de maneira a produzir energia suficiente para enviar um impulso eletrico à central telefónica para podermos dizer a um/ uma resposavel, o numero de telefone para o qual queriamos ligar, é por isso que este telefone não tem nenhum marcador de numeros.";
            buildFirstAndroidXMLFile(objectName,win,objectInfoText1, objectInfoText2, objectInfoText3);
            SaveItems();
        }
        else
        {
            string objectName = "Telephone";
            bool win = true;
            string objectInfoText1 = "Antigamente, quando se queria telefonar a alguem, era necessário rodar a manivela ao lado das bobinas vermelhas, de maneira a produzir energia suficiente para enviar um impulso eletrico à central telefónica para podermos dizer a um/uma resposavel, o numero de telefone para o qual queriamos ligar, é por isso que este telefone não tem nenhum marcador de numeros.";
            string objectInfoText2 = "Antigamente, quando se queria telefonar a alguem, era necessário rodar a manivela ao lado das bobinas vermelhas, de maneira a produzir energia suficiente para enviar um impulso eletrico à central telefónica para podermos dizer a um/ uma resposavel, o numero de telefone para o qual queriamos ligar, é por isso que este telefone não tem nenhum marcador de numeros.";
            string objectInfoText3 = "Antigamente, quando se queria telefonar a alguem, era necessário rodar a manivela ao lado das bobinas vermelhas, de maneira a produzir energia suficiente para enviar um impulso eletrico à central telefónica para podermos dizer a um/ uma resposavel, o numero de telefone para o qual queriamos ligar, é por isso que este telefone não tem nenhum marcador de numeros.";
            buildFirstAndroidXMLFile(objectName, win, objectInfoText1, objectInfoText2, objectInfoText3);
            SaveItems();
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
       
        string filename = "";
        //ANDROID
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            filename = "jar:file://" + Application.persistentDataPath + "!/assets/item_data.xml";
        }
        //DESKTOP
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            filename = Application.persistentDataPath + "/StreamingAssets/XML/item_data.xml";
        }
        
        //FileStream stream = new FileStream(Application.persistentDataPath + "/StreamingAssets/XML/item_data.xml", FileMode.Create);
        StreamWriter stream = new StreamWriter(filename);
        serializer.Serialize(stream, itemDB);
        stream.Close();



    }

    public void buildFirstAndroidXMLFile(string objectName, bool win, string objectInfoText1, string objectInfoText2, string objectInfoText3)
    {
        MuseumObject obj = new MuseumObject();
        obj.objectName = objectName;
        obj.earnedSticker = win;
        obj.objectInfoText1 = objectInfoText1;
        obj.objectInfoText2 = objectInfoText2;
        obj.objectInfoText3 = objectInfoText3;
        itemDB.list.Add(obj);

        
    }
    public void loadResetItems()
    {
        //open a new xmlFile
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/XML/item_dataReset.xml", FileMode.Open);
        itemDB = serializer.Deserialize(stream) as ItemDatabase;
        stream.Close();



    }

    //load function
    public void LoadItems()
    {
        //open a new xmlFile
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));

        string filename = "";
        //ANDROID
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            filename = "jar:file://" + Application.persistentDataPath + "!/assets/item_data.xml";
        }
        //DESKTOP
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            filename = Application.persistentDataPath + "/StreamingAssets/XML/item_data.xml";
        }

        FileStream stream = new FileStream(filename, FileMode.Open);
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