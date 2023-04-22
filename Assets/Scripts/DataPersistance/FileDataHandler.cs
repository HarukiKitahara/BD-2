using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.IO;
namespace MyProject.DataPersistance
{
    /// <summary>
    /// GameData�뱾���ļ����໥ת��
    /// </summary>
    public class FileDataHandler
    {
        private readonly string _fullPath;
        public FileDataHandler(string fileName, string filePath)
        {
            _fullPath = Path.Combine(filePath, fileName);
        }
        public GameData Load()
        {
            if (!File.Exists(_fullPath)) return null;
            try
            {
                string stringifiedGameData = "";
                using (var stream = new FileStream(_fullPath, FileMode.Open))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        stringifiedGameData = reader.ReadToEnd();
                    }
                }
                return JsonUtility.FromJson<GameData>(stringifiedGameData);
            }
            catch (Exception e)
            {
                Debug.LogError($"����ʧ�ܡ�\n{_fullPath}\n{e}");
                return null;
            }
        }
        public void Save(GameData gameData)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_fullPath));
                var stringifiedGameData = JsonUtility.ToJson(gameData, true);
                using (var stream = new FileStream(_fullPath, FileMode.Create))         // ѧ�����ѣ�usingȷ��stream��������֮���ӵ����������˹س����¡�
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.Write(stringifiedGameData);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"�浵ʧ�ܡ�\n{_fullPath}\n{e}");
            }
        }
    }
}

