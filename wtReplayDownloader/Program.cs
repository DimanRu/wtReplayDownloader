Console.Write("Link to last part of replay: ");
string? link = Console.ReadLine();
Replay replay;
if (!Replay.TryParse(link, out replay))
{
    Console.WriteLine("Incorrect link!");
    Console.ReadKey();
    return;
}

Directory.CreateDirectory(replay.Hash);
for (int i = 0; i < replay.CountOfParts; i++)
{
    
    Console.Write("Part #{0} downloading...", i);
    using (var client = new HttpClient())
    {
        using (var s = client.GetStreamAsync(replay.GetURL(i)))
        {
            using (var fs = new FileStream(replay.GetFileName(i), FileMode.OpenOrCreate))
            {
                try
                {
                    s.Result.CopyTo(fs);
                    Console.WriteLine(" - Done!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" - Error!\n" + ex.Message);
                    Console.ReadKey();
                    return;
                }
            }
        }
    }
}

Console.WriteLine("Download has been finished.");
if (Directory.Exists(Environment.CurrentDirectory + "\\" + replay.Hash))
    System.Diagnostics.Process.Start("explorer.exe", Environment.CurrentDirectory + "\\" + replay.Hash);
Console.ReadKey();