using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataVLCC
{
    public string english;
    public string katakana;
    public string romaji;
}

public class DatabaseVLCC : MonoBehaviour
{
    public static DatabaseVLCC instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }


    public List<DataVLCC> data = new List<DataVLCC>();

    public DataVLCC FindData(string name)
    {
        foreach(DataVLCC x in data)
        {
            if(x.english == name)
            {
                return x;
            }
        }
        return null;
    }

}
