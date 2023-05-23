namespace SPPR_L1;

public class SpprL1
{
    public List<Bussiness> Bussinesses { get; }
    public decimal ServiceCost { get; set; }
    public decimal Fav { get; } = 0.5m;
    public decimal NonFav { get; } = 0.5m;
    public decimal FF { get; }
    public decimal FN { get; }
    public decimal NF { get; }
    public decimal NN { get; }

    public SpprL1(params Bussiness[] bus)
    {
        Bussinesses = bus.ToList();
    }
    
    public SpprL1(decimal fav, decimal nonFav, decimal ff, decimal fn, decimal nf, decimal nn, decimal serviceCost, params Bussiness[] bus)
    {
        Bussinesses = bus.ToList();
        Fav = fav;
        NonFav = nonFav;
        FF = ff;
        FN = fn;
        NF = nf;
        NN = nn;
        ServiceCost = serviceCost;
    }

    private Dictionary<int, decimal> CalculateProfit(decimal? coefF = null, decimal? coefN = null)
    {
        return Bussinesses.Select(x =>
            new { x.Id, Profit = (coefF ?? Fav) * x.FavorableAmount + (coefN ?? NonFav) * x.NonFavorableAmount })
            .ToDictionary(k => k.Id, v => v.Profit);
    }

    public string GetNode_Task1()
    {
        var profits = CalculateProfit(0.5m, 0.5m);
        var nodes = string.Empty;
        Bussinesses.ForEach(x =>
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
        return nodes;
    }

    public string GetDecision_Task1()
    {
        var profits = CalculateProfit(0.5m, 0.5m);
        var root = $"Вершина({profits.Max(x => x.Value).ToString("F1")} г. о.)\n";
        var nodes = GetNode_Task1();
            return root + nodes;
        
    }

    public string GetDecision_Task2()
    {
        var nodes = GetNode_Task1();
        var profits = CalculateProfit(0.5m, 0.5m);
        var fProfits = CalculateProfit(FF, FN);
        nodes += GetNode_Task2("Сприятливий", Fav, fProfits);
        var nProfits = CalculateProfit(NF, NN);
        nodes += GetNode_Task2("Несприятливий", NonFav, nProfits);
        var maxProfit =
            new[] { profits.Max(x => x.Value), 
                fProfits.Max(x => x.Value) * Fav + nProfits.Max(x => x.Value) * NonFav - ServiceCost, }.Max();
        var root = $"Вершина({maxProfit.ToString("F1")} г. о.)\n";
        
        return root + nodes;
    }

    private string GetNode_Task2(string text, decimal coefficient, Dictionary<int, decimal> profits)
    {
        var node = $"|{text}({coefficient}) {profits.Max(x => x.Value).ToString("F1")} г. о.";
        Bussinesses.ForEach(x =>
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

public class Bussiness
{
    public int Id;
    public string Name;
    public decimal FavorableAmount;
    public decimal NonFavorableAmount;
}