using System;
using System.IO;
using SampleJSON.Models;
using TMPro;
using UnityEngine;

namespace SampleJSON.Managers
{
    public class LoadDataManager : MonoBehaviour
    {
        public TeamCollection teamCollection;
        [SerializeField] private GameObject rowPrefab;
        [SerializeField] private GameObject headersPrefab;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private Transform _tableContainer;
        [SerializeField] private Transform _hadersContainer;
        private string _jsonPath;

        private void Awake()
        {
            _jsonPath = Application.dataPath + "/StreamingAssets/JsonChallenge.json";
        }

        private void Start()
        {
            LoadTeamCollection();
        }
        
        private void LoadTeamCollection()
        {
            using (StreamReader stream = new StreamReader(_jsonPath))
            {
                string json = stream.ReadToEnd();
                teamCollection = JsonUtility.FromJson<TeamCollection>(json);
            }
            CreateUIData(teamCollection);
        }

        private void CreateUIData(TeamCollection team)
        {
            _titleText.text = team.Title;
            
            foreach (var data in team.Data)
            {
                GameObject newRow = Instantiate(rowPrefab, _tableContainer);
                newRow.GetComponent<UIModel>().idText.text = data.ID;
                newRow.GetComponent<UIModel>().nameText.text = data.Name;
                newRow.GetComponent<UIModel>().roleText.text = data.Role;
                newRow.GetComponent<UIModel>().nicknameText.text = data.Nickname;
           
            }

            foreach (var header in team.ColumnHeaders)
            {
                     
                GameObject newHeaders = Instantiate(headersPrefab, _hadersContainer);
                newHeaders.GetComponent<UIModel>().idText.text = header.ID.ToUpper();
                newHeaders.GetComponent<UIModel>().nameText.text = header.Name.ToUpper();
                newHeaders.GetComponent<UIModel>().roleText.text = header.Role.ToUpper();
                newHeaders.GetComponent<UIModel>().nicknameText.text = header.Nickname.ToUpper();
            }
        }
    }

    [Serializable]
    public class TeamCollection
    {
        public string Title;
        public ColumnHeaders[] ColumnHeaders;
        public Data[] Data;
    }

    

    [Serializable]
    public class ColumnHeaders
    {
        public string ID;
        public string Name;
        public string Role;
        public string Nickname;
    }
    [Serializable]
    public class Data
    {
        public string ID;
        public string Name;
        public string Role;
        public string Nickname;
    }
}
