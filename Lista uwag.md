# ForDatadeo

Stworzyłam nowy plik ImprovedDataReader w którym:
 - usunęłam niepotrzebne usingi na początku pliku,
 - ImportedObjects zamieniłam na listę, aby było można dodać obiekt w 41 linijce
 - usunęlam niepotrzebny obiekt w linijce 14
 - użyłam bloku try, catch, finally gdyby pojawił sie problem z połączeniem z plikiem oraz by miec pewnosc ze plik zostanie zamkniety
 - błąd w pętli for: powinno być < zamiast <=
 - obiekt jest "czyszczony" zanim zostanie dodany do listy
 - uprościłam przypisanie childernNumber korzystając z LINQ
 - użyłam linq aby znalezc obiekt typu DATABASE, table, column
 - zrezygnowałam z dziedzieczenia. Dziedziczenie po klasie ImportedObjectBaseClass tylko dwóch właściwości w tym przypadku nie jest zasadne, dodałam property do klaso ImportedObiect
 - dodałam brakujące gettery i settery
 - zamieniłam double na int w numberChildren
 - sformatowałam wcięcia, by całość wyglądała schludnie
 
 W pliku Program.cs poprawiłam literówkę w nazwie pliku. 
 
 
