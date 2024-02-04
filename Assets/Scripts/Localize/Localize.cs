using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//ԭʼ�˼����������ӣ�С��Ϸ�ɲ���json���������벻��Ҫ����������ʹ��ָ�ϣ�
//1.�漰�����ֵ����ݣ��ȵ�resource/lang/��Ӧ���Լ��±��а���ʽ���key�ͷ�������
//2.Ȼ����Ҫ��ʾ���ֵĵط�������ճ���������ݣ� $"{Localize.GetInstance().GetTextByKey($"{key}")}"
//3.��key�滻Ϊ�����ı��ж�Ӧ��key
public class Localize
{
    private static Localize m_instance;
    private Dictionary<string, string> m_dic_lt;
    private List<Localize_Txt> m_list_lt;
    private LanguageList m_currentLanguage;

    public static Localize GetInstance()
    {
        if (m_instance == null)
        {
            m_instance = new Localize();
        }
        return m_instance;
    }

    public enum LanguageList
    {
        en,
        cn
    }

    Localize()
    {
        m_dic_lt = new Dictionary<string, string>();
        m_list_lt = new List<Localize_Txt>();
        m_currentLanguage = LanguageList.cn;
        LoadLanguage();
        OnLanguageChanged();
    }

    public void RegisterLT(Localize_Txt elt)
    {
        m_list_lt.Add(elt);
    }

    public void UnregisterLT(Localize_Txt elt)
    {
        m_list_lt.Remove(elt);
    }

    public void ChangeLanguage(LanguageList list)
    {
        if (m_currentLanguage == list) return;
        m_currentLanguage = list;
        m_dic_lt.Clear();
        LoadLanguage();
        OnLanguageChanged();
    }

    public void OnLanguageChanged()
    {
        foreach (var lt in m_list_lt)
        {
            lt.OnLanguageChanged();
        }
    }

    public string GetTextByKey(string key)
    {
        return m_dic_lt[key];
    }

    public void LoadLanguage()
    {
        switch (m_currentLanguage)
        {
            case LanguageList.en:
                {
                    LoadLanguageFile("Lang/en");
                    break;
                }
            case LanguageList.cn:
                {
                    LoadLanguageFile("Lang/cn");
                    break;
                }
        }
    }
    public void LoadLanguageFile(string filename)
    {
        TextAsset asset = Resources.Load(filename) as TextAsset;
        Stream st = new MemoryStream(asset.bytes);
        StreamReader sr = new StreamReader(st);
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            string[] tempStrings = line.Split('$');
            m_dic_lt[tempStrings[0]] = tempStrings[1];
            //Debug.Log(line);
        }
    }
}
