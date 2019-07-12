using System.Collections.Generic;

[System.Serializable]
public class Currency
{
	public List<CurrencyFields> banknotes = new List<CurrencyFields>();
	public List<CurrencyFields> coins = new List<CurrencyFields>();
	public string currency;
}
