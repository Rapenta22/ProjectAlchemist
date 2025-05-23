using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HPUI : MonoBehaviour
{
    public List<Image> hpImages; // �� 9�� ���� (�����Ϳ��� ������� �ֱ�)
    public Sprite fullHPSprite;
    public Sprite emptyHPSprite;
    public Sprite lockedHPSprite;

    public int maxHP = 4;     // ���� �ִ� HP
    public int currentHP = 4; // ���� ���� HP

    public void SetHP(int current, int max)
    {
        currentHP = current;
        maxHP = max;

        for (int i = 0; i < hpImages.Count; i++)
        {
            if (i < maxHP)
            {
                hpImages[i].enabled = true;
                hpImages[i].sprite = (i < currentHP) ? fullHPSprite : emptyHPSprite;
            }
            else
            {
                hpImages[i].enabled = false; // �Ǵ� lockedHPSprite�� ��� ���� ǥ��
                // hpImages[i].sprite = lockedHPSprite;
            }
        }
    }

    public void AddMaxHP()
    {
        if (maxHP < hpImages.Count)
            maxHP++;

        // ü�µ� �Բ� ������ų�� ���δ� ����
        if (currentHP < maxHP)
            currentHP++;

        SetHP(currentHP, maxHP);
    }
}

