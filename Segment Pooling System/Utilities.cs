using System;
using System.Collections.Generic;
using System.Linq;

namespace SegmentPoolingSystem {
public static class Utilities {
	public static T[] ShuffleArray<T>(T[] array, int seed) {
		System.Random prng = new System.Random (seed);

		for (int i =0; i < array.Length -1; i ++) {
			int randomIndex = prng.Next(i,array.Length);
			T tempItem = array[randomIndex];
			array[randomIndex] = array[i];
			array[i] = tempItem;
		}
		return array;
	}

    public static int[] ShuffleArray(int start, int count) {
		System.Random prng = new System.Random();
        int[] array = Enumerable.Range(start, count).ToArray();

		for (int i =0; i < array.Length -1; i ++) {
			int randomIndex = prng.Next(i,array.Length);
			int tempItem = array[randomIndex];
			array[randomIndex] = array[i];
			array[i] = tempItem;
		}
		return array;
	}

	public static IEnumerable<IEnumerable<T>> GetKCombsWithRept<T>(IEnumerable<T> list, int length) where T : IComparable {
		if (length == 1) return list.Select(t => new T[] { t });
		return GetKCombsWithRept(list, length - 1)
			.SelectMany(t => list.Where(o => o.CompareTo(t.Last()) >= 0), 
				(t1, t2) => t1.Concat(new T[] { t2 }));
	}
}
}