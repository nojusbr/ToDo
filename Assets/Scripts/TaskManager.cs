using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using System.IO;
using Newtonsoft.Json;
using System.Xml;

public class TaskManager : MonoBehaviour
{
    public TaskUI taskUiPrefab;
    public Transform taskParent;
    public TMP_InputField descriptionInput;
    public string propertyToDelete = "descriptionText";

    private List<TaskUI> tasks = new();

    private void Start()
    {
        LoadFromJson();
    }

    public void GetInput()
    {
        if (string.IsNullOrWhiteSpace(descriptionInput.text)) return;

        Task task = new Task()
        {
            Description = descriptionInput.text,
            Date = DateTime.Now.ToString("yyyy - MM - dd"),
            Finished = false
        };

        AddTask(task);
        descriptionInput.text = "";
    }

    void AddTask(Task task)
    {
        TaskUI taskUi = Instantiate(taskUiPrefab, taskParent);
        taskUi.SetTask(task);
        tasks.Add(taskUi);
    }

    public void SaveToJson()
    {
        if (tasks.Count == 0) return;

        List<Task> list = new();
        foreach (var taskUI in tasks)
        {
            list.Add(taskUI.task);
        }

        //string json = JsonUtility.ToJson(list);
        string json = JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.Indented);

        string path = Path.Combine(Application.persistentDataPath, "tasks.json");
        File.WriteAllText(path, json);

        print(path);
    }

    public void LoadFromJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "tasks.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            //var taskList = JsonUtility.FromJson<List<Task>>(json);
            var taskList = JsonConvert.DeserializeObject<List<Task>>(json);

            foreach (var task in taskList)
            {
                AddTask(task);
            }
        }
    }

    public void DeleteFromJson()
    {

    }

    private void OnApplicationQuit()
    {
        SaveToJson();
    }

    private void OnApplicationPause(bool pause)
    {
        SaveToJson();
    }
}
