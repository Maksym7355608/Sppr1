namespace SPPR_L1;

public class TreeDecision
{
    public List<Firm> Firms { get; }
    public decimal ServiceCost { get; set; }
    public decimal Favorable { get; } = 0.5m;
    public decimal NonFavorable { get; } = 0.5m;
    public decimal FF { get; }
    public decimal FN { get; }
    public decimal NF { get; }
    public decimal NN { get; }

    public TreeDecision(params Firm[] firms)
    {
        Firms = firms.ToList();
    }
    
    public TreeDecision(decimal favorable, decimal nonFavorable, decimal ff, decimal fn, decimal nf, decimal nn, decimal serviceCost, params Firm[] firms)
    {
        Firms = firms.ToList();
        Favorable = favorable;
        NonFavorable = nonFavorable;
        FF = ff;
        FN = fn;
        NF = nf;
        NN = nn;
        ServiceCost = serviceCost;
    }

    private Dictionary<int, decimal> CalculateProfit(decimal? coefF = null, decimal? coefN = null)
    {
        return Firms.Select(x =>
            new { x.Id, Profit = (coefF ?? Favorable) * x.FavorableAmount + (coefN ?? NonFavorable) * x.NonFavorableAmount })
            .ToDictionary(k => k.Id, v => v.Profit);
    }

    public string GetDecisinTreeForTask1_2(bool task2)
    {
        var profits = CalculateProfit(0.5m, 0.5m);
        var root = $"Вершина({profits.Max(x => x.Value).ToString("F1")} г. о.)\n";
        var nodes = string.Empty;
        Firms.ForEach(x =>
        {
            var profit = profits[x.Id];
            var nameAsWhiteSpase = new string(' ', x.Name.Length+profit.ToString("F1").Length+2);
            nodes +=
$@"|
|---{x.Name}({profit.ToString("F1")})---
|      {nameAsWhiteSpase}|---сприятливий стан---`{x.FavorableAmount}` г.о.
|      {nameAsWhiteSpase}|---несприятливий стан---`{x.NonFavorableAmount}` г.о.
|
";
        });
        if (!task2)
            return root + nodes;
        var fProfits = CalculateProfit(FF, FN);
        nodes += GetNodeTree("Сприятливий", Favorable, fProfits);
        var nProfits = CalculateProfit(NF, NN);
        nodes += GetNodeTree("Несприятливий", NonFavorable, nProfits);
        var maxProfit =
            new[] { profits.Max(x => x.Value), 
                fProfits.Max(x => x.Value) * Favorable + nProfits.Max(x => x.Value) * NonFavorable - ServiceCost, }.Max();
        root = $"Вершина({maxProfit.ToString("F1")} г. о.)\n";
        
        return root + nodes;
    }

    private string GetNodeTree(string text, decimal coefficient, Dictionary<int, decimal> profits)
    {
        var node = $"|{text}({coefficient}) {profits.Max(x => x.Value).ToString("F1")} г. о.";
        Firms.ForEach(x =>
        {
            var profit = profits[x.Id];
            var nameAsWhiteSpase = new string(' ', x.Name.Length+profit.ToString("F1").Length+2);
            node +=
                $@"
|
|---{x.Name}({profit.ToString("F1")})---
|      {nameAsWhiteSpase}|---сприятливий стан---`{x.FavorableAmount}` г.о.
|      {nameAsWhiteSpase}|---несприятливий стан---`{x.NonFavorableAmount}` г.о.
|
";
        });
        return node;
    }
}

public record Firm(int Id, string Name, decimal FavorableAmount, decimal NonFavorableAmount);
