using System.Collections.Generic;

var sequenciaMaior = new List<long>();

for (long i = 1; i < 1000000; i++)
{
    calcula(i);
}

exibir();


void calcula(long numero)
{
    var sequeniaAtual = new List<long>();

    sequeniaAtual.Add(numero);

    do
    {
        if (numero % 2 == 0)
        {
            numero = numero / 2;
            sequeniaAtual.Add(numero);
        }
        else
        {
            numero = numero * 3 + 1;
            sequeniaAtual.Add(numero);
        }
    } while (numero != 1);

    if(sequeniaAtual.Count > sequenciaMaior.Count)
        sequenciaMaior = sequeniaAtual;

}

void exibir()
{
    Console.WriteLine("A maior sequencia é do número: " + sequenciaMaior[0]);
    foreach (long i in sequenciaMaior)
    {
        Console.Write(i + ", ");
    }
}




Console.WriteLine("\n\n");