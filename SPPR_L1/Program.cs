using SPPR_L1;

var firms = new []
{
    new Firm(1, "Створення великого виробництва", 400_000m, -200_000m),
    new Firm(2, "Створення малого підприємства", 250_000m, -50_000m),
    new Firm(3, "Створення патенту", 60_000m, 60_000m),
};

var tree = new TreeDecision(firms);
Console.Write(tree.GetDecisinTreeForTask1_2(false));

Console.WriteLine();
Console.WriteLine();
Console.WriteLine();

var firms1 = new []
{
    new Firm(1, "Створення великого виробництва", 600_000m, -300_000m),
    new Firm(2, "Створення малого підприємства", 350_000m, -80_000m),
    new Firm(3, "Створення патенту", 60_000m, 60_000m),
};

var tree1 = new TreeDecision(0.75m, 0.25m, 0.7m, 0.3m, 0.25m, 0.75m, 20_000m, firms1);
Console.Write(tree1.GetDecisinTreeForTask1_2(true));




// var bussinesses = new []
// {
//     new Bussiness { Id = 1, Name = "Створення великого виробництва", FavorableAmount = 400_000m, NonFavorableAmount = -200_000m },
//     new Bussiness { Id = 2, Name = "Створення малого підприємства", FavorableAmount = 250_000m, NonFavorableAmount = -50_000m },
//     new Bussiness { Id = 3, Name = "Створення патенту", FavorableAmount = 60_000m, NonFavorableAmount = 60_000m },
// };
//
// var tree = new SpprL1(bussinesses);
// Console.Write(tree.GetDecision_Task1());
//
// Console.WriteLine();
// Console.WriteLine();
// Console.WriteLine();
//
//
// var tree1 = new SpprL1(0.75m, 0.25m, 0.85m, 0.15m, 0.25m, 0.75m, 10_000m, bussinesses);
// Console.Write(tree1.GetDecision_Task2());