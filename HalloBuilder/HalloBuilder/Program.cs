using HalloBuilder;

Console.WriteLine("Hello, World!");

var schrank = new Schrank.Builder()
                         .SetAnzBöden(4)
                         .SetAnzTüren(6)
                         .Create();

var schrank2 = new Schrank.Builder()
                         .SetAnzTüren(6)
                         .SetAnzBöden(12)
                         .Create();