using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#region Class: RankingData
/// <summary>
/// ��ŷ ������ ���� �̸��� �ð� ����� �����ϴ� ������ Ŭ����
/// </summary>
public class RankingData
{
    public List<string> names = new();
    public List<string> times = new();
}
#endregion

#region Class: Ranking
/// <summary>
/// �̱������� ������ ��ŷ ���� Ŭ����
/// JSON ���Ϸ� �ε�/�����ϸ�, UI�� ���� 5������ ǥ���Ѵ�.
/// </summary>
public class Ranking : Singleton<Ranking>
{
    #region Serialized Fields
    [Header("UI Elements")]
    public Text rankingText;     // ��ŷ�� ����� UI �ؽ�Ʈ

    [Header("Player Info")]
    public string playerName;    // ������ �÷��̾� �̸�
    public float playerTime;     // ������ �÷��̾� ���
    #endregion

    #region Data
    public RankingData data;     // ��ŷ ������
    #endregion

    #region Data Persistence
    /// <summary>
    /// Application.persistentDataPath�� Ranking.json�� �о� �����͸� �ε�
    /// ������ ������ �� �����͸� �����Ѵ�
    /// </summary>
    public void LoadData()
    {
        string path = Path.Combine(Application.persistentDataPath, "Ranking.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<RankingData>(json);

            int count = Mathf.Min(data.names.Count, data.times.Count);
            data.names = data.names.Take(count).ToList();
            data.times = data.times.Take(count).ToList();
        }
        else
        {
            data = new RankingData();
        }

        UpdateUI();
    }

    /// <summary>
    /// ���� playerName�� playerTime�� �����Ϳ� �߰� �� �����Ͽ� ����
    /// �ִ� 5�������� �����Ѵ�
    /// </summary>
    public void SaveData()
    {
        data.names.Add(playerName);
        data.times.Add(playerTime.ToString());

        SortData();

        if (data.names.Count > 5)
        {
            data.names.RemoveAt(5);
            data.times.RemoveAt(5);
        }

        string json = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.persistentDataPath, "Ranking.json");
        File.WriteAllText(path, json);

        UpdateUI();
    }
    #endregion

    #region UI Update
    /// <summary>
    /// ��ŷ�� �ؽ�Ʈ�� �����Ͽ� rankingText�� ǥ��
    /// </summary>
    public void UpdateUI()
    {
        rankingText.text = string.Empty;
        int displayCount = Mathf.Min(5, data.names.Count);

        for (int i = 0; i < 5; i++)
        {
            if (i < displayCount && float.TryParse(data.times[i], out float secs))
            {
                int min = Mathf.FloorToInt(secs / 60f);
                int sec = Mathf.FloorToInt(secs % 60f);
                rankingText.text += $"{i + 1}��: {data.names[i]}   {min:00}:{sec:00}\n";
            }
            else
            {
                rankingText.text += $"{i + 1}��: �Էµ� ������ ����\n";
            }
        }
    }
    #endregion

    #region Data Sorting
    /// <summary>
    /// �̸��� �ð��� Ʃ�÷� ���� �ð� ������ ������ �� ���� 5���� �����
    /// </summary>
    public void SortData()
    {
        var entries = new List<(string name, float time)>();

        for (int i = 0; i < data.names.Count; i++)
        {
            if (float.TryParse(data.times[i], out float t))
                entries.Add((data.names[i], t));
        }

        var top5 = entries.OrderBy(e => e.time).Take(5).ToList();
        data.names = top5.Select(e => e.name).ToList();
        data.times = top5.Select(e => e.time.ToString()).ToList();
    }
    #endregion
}
#endregion
