﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Subtegral.SaveUtility
{
    public class JsonSaveService<T> : SaveService<T> where T : new()
    {
        private string _filePath;

        private void OnEnable()
        {
            _filePath = Path.Combine(Application.persistentDataPath, "userdata.json");
        }

        public override T LoadData()
        {
            if (!File.Exists(_filePath)) return new T();
            var file = File.Open(_filePath, FileMode.Open);
            var deserializedData = JsonUtility.FromJson<T>(file);
            file.Close();
            return deserializedData;
        }

        public override void SaveData(T data)
        {
            var serializedData = JsonUtility.ToJson(data, true);
            File.WriteAllText(_filePath, serializedData);
        }
    }
}