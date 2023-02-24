namespace ConsoleApp
{
    using System; // usunęłam niepotrzebne usingi
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class ImprovedDataReader
    {
        List<ImportedObject1> ImportedObjects; // zamieniłam na listę, aby było można dodać obiekt w 41 linijce

        public void ImportAndPrintData(string fileToImport, bool printData = true)
        {
            ImportedObjects = new List<ImportedObject1>(); // usunęlam niepotrzebny obiekt
            StreamReader streamReader = null;
            try
            {

                streamReader = new StreamReader(fileToImport);


                var importedLines = new List<string>();
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    importedLines.Add(line);
                }

                for (int i = 0; i < importedLines.Count; i++) // powinno być < zamiast <=
                {
                    var importedLine = importedLines[i];
                    var values = importedLine.Split(';');
                    var importedObject = new ImportedObject1();
                    importedObject.Type = values[0].Trim().Replace(" ", "").ToUpper();
                    importedObject.Name = values[1].Trim().Replace(" ", "");
                    importedObject.Schema = values[2].Trim().Replace(" ", "");
                    importedObject.ParentName = values[3].Trim().Replace(" ", "");
                    importedObject.ParentType = values[4].Trim().Replace(" ", "").ToUpper();
                    importedObject.DataType = values[5].Trim().Replace(" ", "");
                    importedObject.IsNullable = values[6].Trim().Replace(" ", "");
                    ImportedObjects.Add(importedObject);
                }

                // assign number of children
                foreach (var importedObject in ImportedObjects)
                {
                    importedObject.ParentType = importedObject.ParentType.Trim().Replace(Environment.NewLine, "");

                    importedObject.NumberOfChildren = ImportedObjects.Count(obj =>  // uproszczenie
                        obj.ParentType == importedObject.Type &&
                        obj.ParentName == importedObject.Name);
                }

                foreach (var database in ImportedObjects.Where(obj => obj.Type == "DATABASE")) // użyłam linq aby znalezc oniekt typu DATABASE
                {
                    Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");

                    // print all database's tables
                    foreach (var table in ImportedObjects.Where(obj => obj.ParentType.ToUpper() == database.Type && obj.ParentName == database.Name)) // use LINQ to filter tables of the current database
                    {
                        Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");

                        // print all table's columns
                        foreach (var column in ImportedObjects.Where(obj => obj.ParentType.ToUpper() == table.Type && obj.ParentName == table.Name)) // use LINQ to filter columns of the current table
                        {
                            Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");
                        }
                    }
                }

                Console.ReadLine();
                Console.WriteLine();

            }
            catch (Exception ex) //gdyby pojawił sie problem z połączeniem z plikiem wyrzuci wyjątek
            {
                Console.WriteLine($"An error occurred while importing data: {ex.Message}");
            }
            finally
            {
                streamReader?.Dispose(); //jakbym pisala w innej wersji to bym użyła usinga, zeby miec pewnosc ze plik zostanie zamkniety
            }
        }

        class ImportedObject1

        {
            public string Name { get; set; } //dziedzieczenie po klasie ImportedObjectBaseClass1 tylko dwóch właściwości w tym przypadku nie jest zasadne
            public string Type { get; set; }
            public string Schema { get; set; } // dodałam brakującą właściwość
            public string ParentName { get; set; } // dodałam brakującą właściwość
            public string DataType { get; set; }
            public string IsNullable { get; set; }
            public int NumberOfChildren { get; set; } // zamieniłam na int
            public string ParentType { get; set; }

        }

    }
}
