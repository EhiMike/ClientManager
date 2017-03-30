using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace support
{
    class CoreGenerator
    {
        private static List<String> defaultAlfabeto = new List<String>();
        private static List<String> revertAlfabeto = new List<String>();
        private static int[] key = { 12,5,20,19,7,15,13,1,4,4,1,6,1,3,3,1};

        public static void initCore()
        {
            // 48-57 = numeri
            //65-90 = lettere
            defaultAlfabeto.Clear();
            revertAlfabeto.Clear();
            for(int i = 65; i < 91; i++)
            {
                char car = (char)i;
                defaultAlfabeto.Add(car.ToString());
            }
            for (int i = 48; i < 58; i++)
            {
                char car = (char)i;
                defaultAlfabeto.Add(car.ToString());
            }
            for(int i = defaultAlfabeto.Count-1; i >= 0; i--)
            {
                revertAlfabeto.Add(defaultAlfabeto[i]);
            }

            Console.WriteLine("Init completed");
        }

        public static string calculate(String input)
        {
            initCore();
            string result = "";
            int somma = 0;
            try
            { 
                if (input.Length == 16)
                {
                    try
                    {
                        foreach (char c in input)
                        {
                            somma += Convert.ToInt32(c - '0');
                        }
                        Console.WriteLine("SOMMA = " + somma);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("errore conversione numero");
                        return "";
                    }
                

                    int[] generatorVal = new int[4];

                    generatorVal[0] = somma / 10;
                    generatorVal[1] = somma % 10;
                    generatorVal[2] = generatorVal[0] + generatorVal[1];
                    generatorVal[3] = -1 * Math.Abs(generatorVal[0] - generatorVal[1]);

                    List<int> valueGenerated = new List<int>();
                    string generatedString = "";
                    for(int i = 0; i < 4; i++)
                    {
                        for(int k = 0; k < 4; k++)
                        {
                            int val = key[4 * i + k]+generatorVal[k];
                            if(val <= 0)
                            {
                                val = 36 + val;
                            }else if(val > 36)
                            {
                                val = val - 36;
                            }
                            valueGenerated.Add(val);
                            generatedString += val.ToString() + "-";
                        }
                    }
                    Console.WriteLine("GENERATED VALUE: " + generatedString);

                    for(int i = 0; i < 16; i++)
                    {
                        string valAlfabeto = revertAlfabeto[valueGenerated[i]-1];
                        result += valAlfabeto;
                    }

                }
                else
                {
                    MessageBox.Show("Errore valore input");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("errore calcolo seriale");
                return "";
            }
            return result;
        }
    }
}
