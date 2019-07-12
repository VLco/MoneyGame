using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combination : MonoBehaviour
{

	List<float> bestComb = null;
	public float _buySum { get; set; }
	public float minPrice;

	public Combination(float buySum)
	{
		_buySum = buySum;
		//  minPrice = _minPrice;
	}

	/* Получеам сумму из набора
	 * param[in] List<int> money - набор денег
	 * return sumPrice - сумма
	*/
	float CalcPrice(List<float> money)
	{
		float sumPrice = 0;
		foreach (float i in money)
		{
			sumPrice += i;
		}
		return sumPrice;
	}

	/* Проверяем набор на наименьшую разницу с требуемой ценой
     * param[in] List<int> money - набор денег
    */
	void CheckSet(List<float> money)
	{
		if (bestComb == null)
		{
			if (CalcPrice(money) >= _buySum)
			{
				bestComb = money;
				minPrice = CalcPrice(money);
			}
		}
		else
		{
			if (CalcPrice(money) >= _buySum && CalcPrice(money) < minPrice)
			{
				bestComb = money;
				minPrice = CalcPrice(money);
			}
		}
	}

	/* Получение лучшей комбинации 
     * param[in] List<float> money - набор денег
    */
	public void MakeAllSets(List<float> money)
	{
		List<float> newSet;


		if (money.Count > 0)
		{
			CheckSet(money);
		}

		for (int i = 0; i < money.Count; i++)
		{
			newSet = new List<float>(money);

			newSet.RemoveAt(i);
			MakeAllSets(newSet);
		}
	}

	public List<float> GetBestSet()
	{
		return bestComb;
	}
}
