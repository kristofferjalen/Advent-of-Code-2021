var input = await File.ReadAllLinesAsync("input-04.txt");

var numbers = input[0].Split(",");

var boards = Enumerable.Range(0, input.Length / 6).Select(x =>
        Enumerable.Range(2, 5).Select(y => input[x * 6 + y].Split(" ", StringSplitOptions.RemoveEmptyEntries))
            .ToArray())
    .ToArray();

bool Bingo(string[][] board, IEnumerable<string> marked)
{
    var bingoInRow = board.Any(row => row.All(marked.Contains));

    var bingoInCol = Enumerable.Range(0, 5).Any(col => board.All(row => marked.Contains(row[col])));

    return bingoInRow || bingoInCol;
}

void Part1()
{
    int m = -1, b = 0;
    var found = false;
    while (!found)
    {
        b = -1;
        m++;
        while (!found && ++b < boards.Length) found = Bingo(boards[b], numbers.Take(m + 1));
    }

    var unmarked = boards[b].SelectMany(x => x).Where(x => !numbers.Take(m + 1).Contains(x)).Select(int.Parse).Sum();

    var score = unmarked * int.Parse(numbers[m]);

    Console.WriteLine(score);
}

void Part2()
{
    var m = -1;
    var boardsWithBingo = new HashSet<int>();
    while (boardsWithBingo.Count < boards.Length)
    {
        m++;
        var b = -1;
        while (boardsWithBingo.Count < boards.Length && ++b < boards.Length)
            if (Bingo(boards[b], numbers.Take(m + 1)))
                boardsWithBingo.Add(b);
    }

    var unmarked = boards[boardsWithBingo.Last()].SelectMany(x => x).Where(x => !numbers.Take(m + 1).Contains(x))
        .Select(int.Parse).Sum();

    var score = unmarked * int.Parse(numbers[m]);

    Console.WriteLine(score);
}

Part1();
Part2();