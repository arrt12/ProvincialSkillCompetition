using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#region Class: RankingData
/// <summary>
/// 랭킹 저장을 위한 이름과 시간 목록을 보관하는 데이터 클래스
/// </summary>
public class RankingData
{
    public List<string> names = new();
    public List<string> times = new();
}
#endregion

#region Class: Ranking
/// <summary>
/// 싱글톤으로 구현된 랭킹 관리 클래스
/// JSON 파일로 로드/저장하며, UI에 상위 5위까지 표시한다.
/// </summary>
public class Ranking : Singleton<Ranking>
{
    #region Serialized Fields
    [Header("UI Elements")]
    public Text rankingText;     // 랭킹을 출력할 UI 텍스트

    [Header("Player Info")]
    public string playerName;    // 저장할 플레이어 이름
    public float playerTime;     // 저장할 플레이어 기록
    #endregion

    #region Data
    public RankingData data;     // 랭킹 데이터
    #endregion

    #region Data Persistence
    /// <summary>
    /// Application.persistentDataPath의 Ranking.json을 읽어 데이터를 로드
    /// 파일이 없으면 새 데이터를 생성한다
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
    /// 현재 playerName과 playerTime을 데이터에 추가 후 정렬하여 저장
    /// 최대 5개까지만 유지한다
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
    /// 랭킹을 텍스트로 포맷하여 rankingText에 표시
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
                rankingText.text += $"{i + 1}위: {data.names[i]}   {min:00}:{sec:00}\n";
            }
            else
            {
                rankingText.text += $"{i + 1}위: 입력된 정보가 없음\n";
            }
        }
    }
    #endregion

    #region Data Sorting
    /// <summary>
    /// 이름과 시간을 튜플로 묶어 시간 순으로 정렬한 뒤 상위 5개만 남긴다
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
