using System;
using System.Collections.Generic;

public static class Utility {
	public static List<T> Shuffle<T>(List<T> list) {
		Random prng = new Random();

		for (int i =0; i < list.Count -1; i ++) {
			int randomIndex = prng.Next(i,list.Count);
			T tempItem = list[randomIndex];
			list[randomIndex] = list[i];
			list[i] = tempItem;
		}

		return list;
	}

}