using System;
using System.Collections;
using System.Collections.Generic;

class App
{
    public static void Main(string[] args)
    {
        Exercise1 team  = new Exercise1()
        {
            Manager = "Adam",
            MemberA = "Ola",
            MemberB = "Ewa"
        };

        foreach(var member in team)
        {
            Console.WriteLine(member);
        }
    }
}
//Cwiczenie 1 (2 punkty)
//Zmodyfikuj klasę Exercise1 aby implementowała interfesj IEnumerable<T>
//Zdefiniuj metodę GetEnumerator, aby zwracał kolejno pola Manager, MemberA, MemberB
//Przykład
//Exercise1<string> team = new Exercise1(){ Manager: "Adam", MemberA: "Ola", MemberB: "Ewa"};
//foreach(var member in team){
//    Console.WriteLine(member);
//}
//otrzymamy na ekranie
//Adam
//Ola
//Ewa
public class Exercise1: IEnumerable<string>
{
    public string Manager { get; init; }
    public string MemberA { get; init; }
    public string MemberB { get; init; }

    public IEnumerator<string> GetEnumerator()
    {
        yield return Manager;
        yield return MemberA;
        yield return MemberB;

    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    
}

public class Exercise1Enumerator : IEnumerator<string>
{
    private Exercise1 _exercise1;
    int counter = -1;
    public string Current
    {
        get
        {
            return counter switch
            {
                0 => _exercise1.Manager,
                1 => _exercise1.MemberA,
                2 => _exercise1.MemberB
            };
        }
    }


    object IEnumerator.Current => throw new NotImplementedException();

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public bool MoveNext()
    {
        return ++counter < 2;
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }
}


//Cwiczenie 2 (2 punkty)
//Zdefiniuj indekser dla klasy CurrencyRates, aby umożliwiał przypisania i pobierania notowania dla danej waluty.
//Zainicjuj tablice _rates, aby jej rozmiar byl równy liczbie stałych wyliczeniowych w klasie Currency i nie wymagał zmiany
//gdy zostaną dodane następne stałe.
//Przykład
//CurrencyRates rates = new CurrencyRates();
//rates[Currency.EUR] = 4.6m;
//Console.WriteLine(rates[Currency.EUR]);

enum Currency
{
    PLN,
    USD,
    EUR,
    CHF
}

class CurrencyRates
{
    //utwórz tablicę o rozmiarze równym liczbie stalych wyliczeniowych Currency
    private decimal[] _rates = new decimal[Enum.GetValues<Currency>().Length];
}

//Cwiczenie 3
//Zdefiniuj enumerator zwracający kolejne liczby szesnastowe zapisane w łańcuchu o długości 8 znaków plus znaki 0x jako prefiks
//Przykład 
//0x00000000 0x00000001 0x00000002 0x00000003 ... 0x0000000A ... 0x000000010 ... 0xFFFFFFFF 
//Zdefiniuj metodę GetLimitedHex(int digitCount), która zwraca enumerator z liczbami o podanej liczbie cyfr.
//Przykład wykorzystania metody
// var limitedHex = hex.GetLimitedHex(4);
// while (limitedHex.MoveNext())
// {
//     Console.WriteLine(limitedHex.Current);
// }
//Wyjście:
//0x0000
//0x0001
//...
//0x2c7b
//0x2c7c
//0x2c7d
//...
//0xffff

class Exercise3 : IEnumerable<string>
{
    public IEnumerator<string> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<string> GetLimitedHex(int digitCount)
    {
        throw new NotImplementedException();
    }
}

enum ChessPiece
{
    Empty,
    King,
    Queen,
    Rook,
    Bishop,
    Knight,
    Pawn
}

enum ChessColor
{
    Black,
    White
}

//Cwiczenie 4 (4 punkty)
//Zdefiniuj klasę opisująca szachownicę z indekserem umożliwiającym dostęp do pola
//na podstawie indeksu w postaci łańcucha np.: "A5" oznacza pierwszą kolumnę i 5 wiersz (od dołu).
//W podanych współrzędnych należy umieścić krotkę z dwoma stałymi wyliczeniowymi (ChessPiece, ChessColor)
//Przykład
//Exercise4 board = new Exerceise4();
//board["A5"] = (ChessPiece.King, ChessColor.White);
//Console.WriteLine(board["A8"]); // pole bez figury w kolorze białym (ChessPiece.Empty, ChessColor.White)
//Console.WriteLine(board["A1"]); // pole bez figury w kolorze czarnym (ChessPiece.Empty, ChessColor.Black)
//Klasa powinna zachować zasadę, że nie można umieścić więcej niż dozwolona liczba figur danego typu:
//1 królowa i król, 2 wieże, gońce, skoczki, 8 pionów
//W sytuacji, gdy zostanie dodana nadmiarowa figura np. 3 skoczek w kolorze białym, klasa powinna zgłosić wyjątek InvalidChessPieceCount
//W sytuacji podania niepoprawnych współrzednych np. K9 lub AA44, klasa powinna zgłosić wyjątek InvalidChessBoardCoordinates 
class Exercise4
{
    private (ChessPiece, ChessColor)[,] _board = new (ChessPiece, ChessColor)[8, 8];
}

class InvalidChessPieceCount : Exception
{

}

class InvalidChessBoardCoordinates : Exception
{

}