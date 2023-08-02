using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.TestData
{
    public static class TestDataGen
    {
       static List<string> msglist = new List<string>()
            {
                "{{product}} at {{unitprice}}p",
                "{{salesquantity}} sales of {{product}}s at {{unitprice}}p each",
                "{{operation}} {{unitprice}}p {{product}}s",
            };

        static List<string> products = new List<string>() { "apple", "mango", "orange" };
        static List<string> unitprice = new List<string>() { "10.45", "20.678", "30.456" };
        static List<string> salesquantity = new List<string>() { "10.09", "20.00", "30.98" };
        static List<string> operations = new List<string>() { "Add", "Multiply", "Substract" };

        public static List<string> generate()
        {
            var rand = new Random();
            var list = new List<string>();

            for(int i=0;i < 3; i++)
            {
                var gen = rand.Next(0, 1);
                string s = msglist[gen].Replace("{{product}}", products[gen]).Replace("{{unitprice}}", unitprice[gen]).Replace("{{salesquantity}}", salesquantity[gen]).Replace("{{operation}}", operations[gen]);
                list.Add(s);

                if(list.Count > 0 && list.Count % 2 == 0)
                {
                    string s1 = msglist[2].Replace("{{product}}", products[gen]).Replace("{{unitprice}}", unitprice[gen]).Replace("{{salesquantity}}", salesquantity[gen]).Replace("{{operation}}", operations[gen]);
                    list.Add(s1);
                }
            }

            return list;

        }


    }
}
