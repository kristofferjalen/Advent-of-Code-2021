var numbers = (await File.ReadAllLinesAsync("input-01.txt")).Select(int.Parse).ToArray();

void Part1()
{
    var sums = Enumerable.Range(1, numbers.Length - 1).Sum(i => numbers[i] > numbers[i - 1] ? 1 : 0);

    Console.WriteLine(sums); // 1482
}

void Part2()
{
    var sums = Enumerable.Range(3, numbers.Length - 3).Sum(i =>
        numbers[i] + numbers[i - 1] + numbers[i - 2]
        > numbers[i - 1] + numbers[i - 2] + numbers[i - 3]
            ? 1 : 0);

    Console.WriteLine(sums); // 1518
}

Part1();
Part2();