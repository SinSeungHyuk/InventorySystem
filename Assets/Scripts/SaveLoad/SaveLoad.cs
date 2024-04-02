using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public static class SaveLoad // ����,�ε� ����� �����Ǿ��ִ� ����ƽ Ŭ����
{
    public static UnityAction OnSaveGame; // ������ ����
    public static UnityAction<SaveData> OnLoadGame; // SaveData���� ID ���Ͽ� ������ �ε�

    private static string directory = "/SaveData/"; // ���� ��ġ
    private static string fileName = "SaveGame.sav"; // ���� �̸�


    public static bool Save(SaveData data)
    {
        OnSaveGame?.Invoke(); // ���� �̺�Ʈ ȣ��

        string dir = Application.persistentDataPath + directory;

        if (!Directory.Exists(dir)) // �ش� ��ΰ� ������ ��������
            Directory.CreateDirectory(dir);

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(dir + fileName, json); // SaveData�� JSon ���ڿ� ���Ϸ� ����

        Debug.Log("Saving game");

        return true;
    }

    public static SaveData Load()
    {   // �����س��� ��ġ
        string fullPath = Application.persistentDataPath + directory + fileName;

        SaveData data = new SaveData();

        if (File.Exists(fullPath))  // �ش� ��η� �̵�
        {
            string json = File.ReadAllText(fullPath);
            data = JsonUtility.FromJson<SaveData>(json); // JSon ������ �о SaveData Ÿ������ ��ȯ

            OnLoadGame?.Invoke(data); // �ε���� �̺�Ʈ ȣ��
        }
        else Debug.Log("���̺� ���� ����");

        return data; // data(���̺�)�� �����ϸ� Ʈ��
    }

    public static void DeleteSaveData()
    {
        string fullPath = Application.persistentDataPath + directory + fileName;

        if (File.Exists(fullPath)) File.Delete(fullPath); // �ش� ����� ���� ����
    }
}
