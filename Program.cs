using System;
using System.Globalization;
using System.Linq;

namespace CreditSuisse
{
    class Program
    {
        static void Main(string[] args)
        {
            Trade trade = new Trade();
            CultureInfo provider = CultureInfo.InvariantCulture;
            string format = "MM/dd/yyyy";
            
            /*
            string input = @"12/11/2020
                             4
                             2000000 Private 12/29/2025
                             400000 Public 07/01/2020
                             5000000 Public 01/02/2024
                             3000000 Public 10/26/2023";

            */
            Console.WriteLine("Please, Inform the Reference Date:");
            trade.ReferenceDate = DateTime.ParseExact(Console.ReadLine(), format, provider);

            Console.WriteLine("Inform the number of trades in portfolio:");
            string ret = Console.ReadLine();

            while (trade.NumLines == 0 && !ret.All(char.IsDigit))
            {
                Console.WriteLine("Inform a valid number of trades in portfolio");
                ret = Console.ReadLine();
            }

            trade.NumLines = Convert.ToInt32(ret);

            string answer = "";
            answer = ReadLinePortfolio(trade, provider, format, ret, answer);

            Console.WriteLine(answer);
            Console.ReadLine();

            static string ReadLinePortfolio(Trade trade, CultureInfo provider, string format, string ret, string answer)
            {
                for (int i = 0; i < trade.NumLines; i++)
                {
                    Console.WriteLine("Enter the portfolio's row " + (i + 1).ToString());
                    ret = Console.ReadLine();

                    while(ret == null || ret == "")
                    {
                        Console.WriteLine("Enter the portfolio's row " + (i + 1).ToString());
                        ret = Console.ReadLine();
                    }

                    string[] rets = ret.Split(' ');
                    trade.Value = Convert.ToDouble(rets[0]);
                    trade.ClientSector = (rets[1]);

                   switch (rets[1]){
                        case "Private": trade.ClientType = ClientType.PRIVATE; break;
                        case "Public": trade.ClientType = ClientType.PUBLIC; break;
                    }
                        
                  
                    trade.NextPaymentDate = DateTime.ParseExact(rets[2], format, provider);

                    if(trade.ReferenceDate.Subtract(trade.NextPaymentDate).TotalDays > 30)
                    {
                        trade.Category = Categories.EXPIRED;
                        answer += "EXPIRED\r\n";
                    }
                    if(trade.Value > 1000000 && trade.ClientType == ClientType.PRIVATE)
                    {
                        answer += "HIGHRISK\r\n";
                    }
                    if (trade.Value > 1000000 && trade.ClientType == ClientType.PUBLIC)
                    {
                        answer += "MEDIUMRISK\r\n";
                    }
                }

                return answer;
            }
        }


    }
}
