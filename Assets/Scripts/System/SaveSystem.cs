using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//也是之前写的存档系统轮子，先搬过来，不用管内容，我会调试
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
