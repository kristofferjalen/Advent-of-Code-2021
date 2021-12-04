var input = await File.ReadAllLinesAsync("input-03.txt");

var numbers = input.Select(x => Convert.ToInt32(x, 2)).ToArray(); // can't use byte (8-bit), need 12 bits (int, 32-bit)

const int length = 12;

void Part1()
{
    double gamma = 0d, epsilon = 0d;

    for (var pos = 0; pos < length; pos++)
    {
        var pos1 = pos;

        var ones = numbers.Sum(n => (n & (1 << pos1)) != 0 ? 1 : 0);

        gamma += ones > numbers.Length - ones ? Math.Pow(2, pos) : 0;

        epsilon += ones < numbers.Length - ones ? Math.Pow(2, pos) : 0;
    }

    Console.WriteLine(gamma * epsilon); // 1092896
}

void Part2()
{
    double Value(Func<List<int>, List<int>, List<int>> f)
    {
        var pos = length - 1;
        var copy = numbers;

        while (copy.Length > 1)
        {
            var zeros = new List<int>();
            var ones = new List<int>();

            foreach (var n in copy)
            {
                if ((n & (1 << pos)) == 0) zeros.Add(n);
                if ((n & (1 << pos)) != 0) ones.Add(n);
            }

            copy = f(zeros, ones).ToArray();

            pos--;
        }

        return copy[0];
    }

    var oxygenRating = Value((zeros, ones) => ones.Count >= zeros.Count ? ones : zeros);

    var co2Scrubber = Value((zeros, ones) => zeros.Count <= ones.Count ? zeros : ones);
    
    Console.WriteLine(oxygenRating * co2Scrubber); // 4672151
}

Part1();
Part2();