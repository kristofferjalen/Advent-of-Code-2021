var input = await File.ReadAllLinesAsync("input-02.txt");

var instructions = input.Select(x => x.Split(" ")).Select(x => (x[0], int.Parse(x[1]))).ToArray();

void Part1()
{
    int h = 0, d = 0;

    foreach (var (dir, units) in instructions)
        switch (dir)
        {
            case "forward":
                h += units;
                break;
            case "down":
                d += units;
                break;
            case "up":
                d -= units;
                break;
        }

    Console.WriteLine(h * d); // 1383564
}

void Part2()
{
    int h = 0, d = 0, aim = 0;

    foreach (var (dir, units) in instructions)
        switch (dir)
        {
            case "forward":
                h += units;
                d += aim * units;
                break;
            case "down":
                aim += units;
                break;
            case "up":
                aim -= units;
                break;
        }

    Console.WriteLine(h * d); // 1488311643
}

Part1();
Part2();