using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//Ҳ��֮ǰд�Ĵ浵ϵͳ���ӣ��Ȱ���������ù����ݣ��һ����
public static class SaveSystem
{

    public static void SaveCoin(Coin coin)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.cheat";
        FileStream stream = new FileStream(path, FileMode.Create);
        CoinData data = new CoinData(coin);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static CoinData LoadCoin()
    {
        string path = Application.persistentDataPath + "/player.cheat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            CoinData data = formatter.Deserialize(stream) as CoinData;
            stream.Close();
            return data;
        }
        else
        {
            return new CoinData(new Coin());
        }
    }
}
