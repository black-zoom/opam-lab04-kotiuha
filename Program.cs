using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static List<int> BuildList(int count, int seed)
    {
        Random rnd = new Random(seed);
        List<int> list = new List<int>(count);

        for (int i = 0; i < count; i++)
            list.Add(rnd.Next(-9, 10));

        return list;
    }

    static List<int> BuildSortedList(int count, int seed)
    {
        Random rnd = new Random(seed);
        List<int> list = new List<int>(count);

        if (count == 0)
            return list;

        list.Add(rnd.Next(1, 10));

        for (int i = 1; i < count; i++)
            list.Add(list[i - 1] + rnd.Next(1, 10));

        return list;
    }

    static string FormatList(List<int> list)
    {
        return "[" + string.Join(", ", list) + "]";
    }

    static bool ListsEqual(List<int> first, List<int> second)
    {
        if (first.Count != second.Count)
            return false;

        for (int i = 0; i < first.Count; i++)
            if (first[i] != second[i])
                return false;

        return true;
    }

    static bool IsSorted(List<int> list)
    {
        for (int i = 1; i < list.Count; i++)
            if (list[i] < list[i - 1])
                return false;

        return true;
    }

    static bool CheckSplit(List<int> original, List<int> first, List<int> second)
    {
        return first.Count + second.Count == original.Count;
    }

    static List<int> MergeLists(List<int> first, List<int> second)
    {
        List<int> result = new List<int>(first.Count + second.Count);
        int i = 0;
        int j = 0;

        while (i < first.Count && j < second.Count)
        {
            if (first[i] <= second[j])
                result.Add(first[i++]);
            else
                result.Add(second[j++]);
        }

        while (i < first.Count)
            result.Add(first[i++]);

        while (j < second.Count)
            result.Add(second[j++]);

        return result;
    }

    static double MeasureAdd(int count, bool presetCapacity)
    {
        List<int> list = presetCapacity
            ? new List<int>(count)
            : new List<int>();

        Stopwatch sw = Stopwatch.StartNew();

        for (int i = 0; i < count; i++)
            list.Add(i);

        sw.Stop();
        return sw.Elapsed.TotalMilliseconds;
    }

    static double MeasureInsert(int count)
    {
        List<int> list = new List<int>(count);
        Stopwatch sw = Stopwatch.StartNew();

        for (int i = 0; i < count; i++)
            list.Insert(0, i);

        sw.Stop();
        return sw.Elapsed.TotalMilliseconds;
    }

    static double MeasureContains(int count, int searches)
    {
        List<int> list = new List<int>(count);

        for (int i = 0; i < count; i++)
            list.Add(i);

        Stopwatch sw = Stopwatch.StartNew();

        for (int i = 0; i < searches; i++)
            list.Contains(-1);

        sw.Stop();
        return sw.Elapsed.TotalMilliseconds;
    }

    static List<int> Task1(List<int> list)
    {
        List<int> result = new List<int>(list.Count);

        for (int i = 0; i < list.Count; i++)
            if (list[i] > 0 && list[i] % 2 != 0)
                result.Add(list[i]);

        return result;
    }

    static void Task2(List<int> original)
    {
        double average = 0;

        if (original.Count > 0)
        {
            for (int i = 0; i < original.Count; i++)
                average += original[i];

            average /= original.Count;
        }

        List<int> wrong = new List<int>(original);

        for (int i = 0; i < wrong.Count; i++)
            if (wrong[i] < average)
                wrong.RemoveAt(i);

        List<int> fromEnd = new List<int>(original);

        for (int i = fromEnd.Count - 1; i >= 0; i--)
            if (fromEnd[i] < average)
                fromEnd.RemoveAt(i);

        List<int> newList = new List<int>(original.Count);

        for (int i = 0; i < original.Count; i++)
            if (original[i] >= average)
                newList.Add(original[i]);

        Console.WriteLine(FormatList(wrong));
        Console.WriteLine(FormatList(fromEnd));
        Console.WriteLine(FormatList(newList));
        Console.WriteLine(ListsEqual(fromEnd, newList));
    }

    static void Task3()
    {
        List<int> first = BuildSortedList(11, 14);
        List<int> second = BuildSortedList(7, 114);
        List<int> merged = MergeLists(first, second);

        Console.WriteLine(FormatList(merged));
        Console.WriteLine(IsSorted(merged));
    }

    static void Task4(List<int> list)
    {
        List<int> first = new List<int>(list.Count);
        List<int> second = new List<int>(list.Count);

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] < 1)
                first.Add(list[i]);
            else
                second.Add(list[i]);
        }

        Console.WriteLine(FormatList(first));
        Console.WriteLine(FormatList(second));
        Console.WriteLine(CheckSplit(list, first, second));
    }

    static void Task5()
    {
        MeasureAdd(1000, false);
        MeasureAdd(1000, true);
        MeasureInsert(1000);
        MeasureContains(1000, 10);

        double add = MeasureAdd(100000, false);
        double addCapacity = MeasureAdd(100000, true);
        double insert = MeasureInsert(100000);
        double contains = MeasureContains(100000, 1000);

        Console.WriteLine($"{add:F2} ms");
        Console.WriteLine($"{addCapacity:F2} ms");
        Console.WriteLine($"{insert:F2} ms");
        Console.WriteLine($"{contains:F2} ms");
        Console.WriteLine($"{(add > 0 ? insert / add : 0):F2}");
    }

    static void Main()
    {
        List<int> list = BuildList(20, 14);

        Console.WriteLine(FormatList(list));

        List<int> result = Task1(list);
        Console.WriteLine(FormatList(result));
        Console.WriteLine(result.Count);
        Console.WriteLine();
        
        Task2(list);
        Console.WriteLine();

        Task3();
        Console.WriteLine();

        Task4(list);
        Console.WriteLine();

        Task5();
    }
}
