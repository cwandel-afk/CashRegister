using Truefit_CashRegister.Services;

/***************************************************************************
 *                                                                         *
 *  CashRegisterService Processes all .txt files in the 'Data' folder.     *
 *  Once this is done I write the answers to the answers.txt file in the   *
 *  'Output' folder, as well as to the Console.                            *
 *                                                                         *
 ***************************************************************************/


public class Program
{
    public static void Main(string[] args)
    {
        string[] data = ProcessFiles();
        List<string> answers = new List<string>();

        for (int i = 0; i < data.Length; i++)
        {
            var inputs = data[i].Split(',');
            if(inputs.Length == 2) 
            {
                double paid = double.Parse(inputs[1].TrimEnd('\r'));
                double total = double.Parse(inputs[0].TrimEnd('\r'));

                /***************************************************************************
                 *                                                                         *
                 *  The CashRegisterService can handle both EUR and USD currencies. To     *
                 *  switch currencies, change CurrencyType.USD to CurrencyType.EUR.        *
                 *  Additionally, it can process any divisor integer. By default, the      *
                 *  divisor is set to 3. To customize the divisor, provide the optional    *
                 *  integer parameter in the constructor call.                             *
                 *                                                                         *
                 ***************************************************************************/
                var register = new CashRegisterService(total, paid, CurrencyType.USD);

                answers.Add(register.GetChange());
            }
        }

        WriteAnswers(answers);
    }

    private static string[] ProcessFiles()
    {
        List<string> data = new List<string>();

        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        Console.WriteLine(filePath);
        foreach(string file in Directory.GetFiles(filePath))
        {
            if (File.Exists(file))
            {
                string content = File.ReadAllText(file);
                data.AddRange(content.Split('\n'));
            }
        }

        return data.ToArray();
    }

    private static void WriteAnswers(List<string> answers)
    {
        try
        {
            string relativePath = @"..\..\..\Output\answers.txt";
            string filePath = Path.GetFullPath(relativePath);

            if(File.Exists(filePath))
            {
                using (StreamWriter writer = new(filePath))
                {
                    foreach (string a in answers)
                    {
                        writer.WriteLine(a);
                        Console.WriteLine(a);
                    }
                }
                Console.WriteLine($"\n\n MESSAGE: \nAnswers are provided in the console log above and {filePath}\n\n");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
        }
    }
}