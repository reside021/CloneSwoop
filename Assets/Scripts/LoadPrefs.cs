using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadPrefs : MonoBehaviour
{
    [SerializeField] private GameObject _panelList;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            if (PlayerPrefs.HasKey($"R{i}"))
            {
                string[] record = TextParse(PlayerPrefs.GetString($"R{i}"));
                var row = _panelList.transform.GetChild(i);
                row.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = record[0];
                row.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = record[1];
            }
        }
    }

    string[] TextParse(string record)
    {
        return record.Split(new char[] { '&' });
    }
}
