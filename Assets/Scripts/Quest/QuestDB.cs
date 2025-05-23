using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/QuestDB")]
public class QuestDB : ScriptableObject
{
    [System.Serializable]
    public class QuestEntry
    {
        public string questId;
        public QuestData questData;
    }

    [System.Serializable]
    public class QuestGroup
    {
        public string groupName; // ��: "Village", "Forest"
        public List<QuestEntry> quests;
    }

    [Header("�׷캰 ����Ʈ")]
    public List<QuestGroup> questGroups;

    /// ����Ʈ ID�� ��ü �׷쿡�� Ž��
    public QuestData GetQuestById(string questId)
    {
        foreach (var group in questGroups)
        {
            foreach (var entry in group.quests)
            {
                if (entry.questId == questId)
                    return entry.questData;
            }
        }
        return null;
    }

    /// Ư�� �׷��� ��� ����Ʈ ��ȯ
    public List<QuestData> GetQuestsByGroup(string groupName)
    {
        return questGroups
            .FirstOrDefault(g => g.groupName == groupName)?.quests
            .Where(e => e.questData != null)
            .Select(e => e.questData)
            .ToList() ?? new List<QuestData>();
    }
}
