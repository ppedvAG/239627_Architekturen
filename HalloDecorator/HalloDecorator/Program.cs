// See https://aka.ms/new-console-template for more information
using HalloDecorator;

Console.WriteLine("Hello, World!");

var brot = new Salami(new Salami( new Brot()));

Console.WriteLine($"{brot.Name} {brot.Price}");