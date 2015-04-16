3.1 Individuellt arbete
=======================
Det tredje steget utgörs uteslutande av ett individuellt arbete (ett gemensamt arbete tillsammans med kursen Databasteknik om du även läser den kursen).

Krav på webbapplikationen till det individuella arbetet
------------------------------------------------------
(Krav kursen Databasteknik ställer på databasen som ingår i det individuella arbetet hittar du på webbplatsen för kursen Databasteknik.)

> **Aktuella krav hittar du på kursens webbplats, https://coursepress.lnu.se/kurs/aspnet-web-forms/individuellt-arbete/.**

1. Det individuella arbetet ska i sin helhet versionshanteras via Git och hanteras på GitHub. Repositoriet du ska använda tillhandahålls av kursledningen.
2. **En idébeskrivning, pdf- eller md-fil med namnet idebeskrivning.pdf|md, ska senast tisdagen den 3 mars 2015 12:00 lags upp i repositorietför ditt individuella arbete. Meddela även kurseldningen att det finns en idébeskrivning genom att skicka ett mejl till 1dv406@lnu.se** med ämnesraden ‘Idébeskrivning VT15′.
	1. Idébeskrivningen ska innehålla:
	2. En kort beskrivning av problemet.
	3. En fysisk datamodell i form av ett databasdiagram om tre tabeller med relationer mellan tabellerna. Exempeldata ska även finnas för samtliga tabeller i datamodellen.
	4. “Mockup”, en eller flera av enklare slag, vars fokus är att beskriva funktion snarare än grafisk design.
3. **Den fullständiga webbapplikationen och databasen måste senast tisdagen den 17 mars 2015 12:00 ha publicerats på publikationsservern FALKEN.**
4. Webbapplikationen ska vara skapad med ASP.NET 4.5 och C#.
5. Webbapplikationen måste bestå av minst två .aspx-sidor.
6. Webbapplikationen måste använda sig av **minst en “master page”**.
7. Webbapplikationen ska vara en s.k. femlagerapplikation. Lager som ska finnas med är:
 	1. Användargränssnittlager
 	2. Presentationslogiklager
 	3. Affärslogiklager
 	4. Dataåtkomstlager
 	5. Datalager
8. Inget lager får “hoppas” över, t.ex. får presentationslogiklagret inte kommunicera direkt med dataåtkomst- eller datalagret.
9. **Användargränssnittlagret (klienten) ska vara av en webbläsare som tillåter JavaScript.** Det räcker att applikationen fungerar på en av följande webbläsare: FireFox, Chrome, IE, Opera eller Safari.
10. Klasser för affärslogik- och dataåtkomstlager ska placeras i separata kataloger: Model respektive DAL.
11. **Affärslogik- och dataåtkomstlagret måste implementeras med C#-klasser som du egenhändigt skriver för hand.**
12. **Dataåtkomstlagret måste använda ADO.NET** för att kommunicera med datalagret.
13. Datalagret måste utgöras av en databas.
14. Webbapplikationen ska minst utgöra ett gränssnitt mot tre tabeller. Det ska vara möjligt att hämta och presentera data från alla tre tabeller. **Via webbformulär ska det för två av tabellerna vara möjligt att lägga till, uppdatera och ta bort poster.**
15. Användaren måste bli informerad med ett meddelande då användaren på något sätt försökt/lyckats påverka datat i tabellerna. Både **”rätt”- och felmeddelande måste visas**.
16. **Allt data måste utan undantag valideras** i så väl användargränssnittlagret, som i presentationslogiklagret, som i affärslogiklagret.
17. **All kommunikation med databasen måste ske genom användaren appUser** som har lösenordet 1Br@Lösen=rd?. Användaren appUser får bara ha exekveringsrättigheter av lagrade procedurer. Rättigheter till andra objekt, som t.ex. tabeller, tillåts inte.
