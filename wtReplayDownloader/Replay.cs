using System.Text.RegularExpressions;

struct Replay
{
    static string pattern = """
(https://wt-game-replays\.warthunder\.com)/([\d\w]+)/(\d{4})\.wrpl
""";

    public string Hash;
    public int CountOfParts;
    public string Address;

    static public bool TryParse(string? param, out Replay replay)
    {
        replay = new Replay();
        if (param == null)
            return false;

        var matches = Regex.Matches(param, pattern);
        if (matches.Count == 0)
            return false;

        replay.Address = matches[0].Groups[1].Value;
        replay.Hash = matches[0].Groups[2].Value;
        replay.CountOfParts = int.Parse(matches[0].Groups[3].Value) + 1;
        return true;
    }

    public string GetURL(int part)
    {
        return Address + "/" + Hash + "/" + part.ToString("D4") + ".wrpl";
    }

    public string GetFileName(int part)
    {
        return Hash + "\\" + part.ToString("D4") + ".wrpl";
    }
}
