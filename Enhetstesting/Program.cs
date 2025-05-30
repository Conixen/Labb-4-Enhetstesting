﻿using Enhetstesting;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static void Main(string[] args)
    {
        LibrarySystem library = new LibrarySystem();
        UserInterface.DisplayMenu(library);
    }
}

//# Vad du ska göra

//Din uppgift är att skriva enhetstester som verifierar att nedanstående krav uppfylls. Tänk särskilt på:

// - Att testa både normalfall och gränsfall
// - Att verifiera att systemet hanterar felaktiga indata på ett korrekt sätt
// - Att testa att systemet följer alla specificerade regler och begränsningar

//    ## 1. Bokhantering

//### 1.1 Lägga till böcker

//Systemet ska kunna hantera tillägg av nya böcker i bibliotekskatalogen.

//- Varje bok måste ha ett ISBN-nummer
//- Systemet ska inte tillåta dubbletter av ISBN-nummer

//### 1.2 Ta bort böcker

//Systemet ska kunna hantera borttagning av böcker från bibliotekskatalogen.

//- Böcker ska kunna tas bort ur systemet
//- Böcker som är utlånade ska inte kunna tas bort från systemet

//### 1.3 Sökning

//Systemet ska erbjuda flera sökfunktioner för att hitta böcker (ISBN, Titel eller författare):

// - Sökning ska vara skiftlägesokänslig (versaler/gemener ska ge samma resultat)
//- Sökningar ska kunna hitta böcker på delmatchningar (inte bara exakta matchningar)

//## 2. Utlåningssystem

//### 2.1 Låna ut böcker

//Systemet ska hantera utlåning av böcker.

//- En bok som lånas ut ska markeras som utlånad i systemet
//- Redan utlånade böcker ska inte kunna lånas ut
//- När en bok lånas ska rätt utlåningsdatum sättas

//### 2.2 Återlämning

//Systemet ska hantera återlämning av böcker.

//- Vid återlämning ska bokens utlåningsdatum nollställas
//- Endast utlånade böcker ska kunna återlämnas

//### 2.3 Förseningshantering

//Systemet ska kunna identifiera och hantera försenade böcker.

//- Korrekt beräkning av om en bok är försenad ska implementeras
//- Förseningsavgifter ska beräknas enligt specificerad formel (förseningsavgift * antal dagar försenad)