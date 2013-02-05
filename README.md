Sokoban
=======

C# opdracht week 2

Schoolopdracht: Sokoban game maken


Doel van het spel Sokoban

Plaats alle kisten in zo min mogelijk tijd en met zo weinig mogelijk bewegingen op de bestemmingen.


Regels

De vorkheftruck kan alleen een kist duwen, niet trekken
De vorkheftruck kan maximaal één kist tegelijkertijd duwen.
De vorkheftruck kan niet door of over een muur of een kist rijden.
De vorkheftruck kan over een bestemming (kruisje) rijden, want dat is gewoon een onderdeel van de vloer.
Een kist kan niet door of op een muur of een andere kist geduwd worden.
Een kist die op een bestemming staat, mag nog verplaatst worden.


Eisen aan het programma

Het programma moet het aantal bewegingen van de vorkheftruck tellen en weergeven.
Het programma moet het aantal verschuivingen van de kisten tellen en weergeven.
De gebruiker kan de vorkheftruck besturen met de pijltjestoetsen op het toetsenbord. 
Bij iedere toetsaanslag verplaatst de truck zich als dat mag volgens de regels 
en verschuift daarbij de kist als er een geduwd kan worden volgens de regels.


Nog wat eisen

De speler kan kiezen uit een aantal vooraf vastgelegde puzzels. Deze zijn opgeslagen in tekstbestanden, zie verderop.
De speeltijd wordt in seconden bijgehouden en weergegeven. De tijd loopt via een DispatcherTimer.
Per puzzel wordt een lijst met high-scores bijgehouden, ook in tekstbestanden.
De score wordt als volgt bepaald: minste bewegingen scoort het hoogst, daarna de korste tijd.
