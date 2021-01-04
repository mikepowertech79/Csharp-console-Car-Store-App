using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csharp_console_Car_Store_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            MessageBox.Show("Start without debugging: press Ctrl + F5, otherwise console would not have any output");
        }


        // Car store app source: https://www.youtube.com/watch?v=P0rOJz8epfg&list=PLhPyEFL5u-i3CoTQC9wPhB9uECUxTxkiu

        //=========================================================================================================================
        //Source: Console.WriteLine does not show up in Output window ok https://stackoverflow.com/questions/2669463/console-writeline-does-not-show-up-in-output-window     Just use Ctrl + F5


        // Console in Windows Forms https://zombievdk.clan.su/news/urok_7_console_v_proekte_windows_forms/2016-07-07-33
        public partial class NativeMethods
        {
            public static Int32 STD_OUTPUT_HANDLE = -11;

            [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "GetStdHandle")]
            public static extern System.IntPtr GetStdHandle(Int32 nStdHandle);

            [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "AllocConsole")]

            [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]

            public static extern bool AllocConsole();
        }




        private void button1_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Start without debugging: press Ctrl + F5, otherwise console would not have any output");
            //inside the Console method 
            consondeb();
        }

        public void consondeb()
        {

            if (NativeMethods.AllocConsole())
            {

                IntPtr stdHandle = NativeMethods.GetStdHandle(NativeMethods.STD_OUTPUT_HANDLE);


                Store s = new Store();

                Console.WriteLine("Welcome to the car store. First you must create some car inventory. Then you may add some cars to the shopping cart. Finally you may checkout which will give you a total value of the shopping cart ");

                int action = chooseAction();

                while (action != 0)
                {
                    Console.WriteLine("You chose " + action);

                    switch (action)
                    {

                        //add item to inventory
                        case 1:
                            Console.WriteLine("You choose to add a new car to the inventory ");
                            string carMake = "";
                            string carModel = "";
                            Decimal carPrice = 0;

                            Console.WriteLine("What is the car make? ford, gm, nissan etc. ");
                            carMake = Console.ReadLine();

                            Console.WriteLine("What is the car model? corvette, focus, ranger etc. ");
                            carModel = Console.ReadLine();

                            Console.WriteLine(("What is the car price? "));
                            carPrice = int.Parse(Console.ReadLine());

                            Car newCar = new Car(carMake, carModel, carPrice);
                            s.CarList.Add(newCar);

                            printInventory(s);
                            break;


                        case 2:
                            Console.WriteLine("You choose to add a car to your shopping cart ");
                            printInventory(s);
                            Console.WriteLine("Which item would you like to buy? (number) ");


                            int carChosen = int.Parse(Console.ReadLine());


                            if (s.CarList.Count! > carChosen)
                            {
                                s.ShoppingList.Add(s.CarList[carChosen]);

                                printShoppingCart(s);
                            }
                            else
                            {
                                Console.WriteLine("No such car in the Car List");
                            }

                            //Denis found https://docs.microsoft.com/ru-ru/dotnet/api/system.argumentoutofrangeexception?view=netcore-3.1#code-try-4

                            //try
                            //{
                            //    Console.WriteLine("The first item: '{0}'", list[0]);
                            //}
                            //catch (ArgumentOutOfRangeException e)
                            //{
                            //    Console.WriteLine(e.Message);
                            //}


                            break;


                        case 3:
                            printShoppingCart(s);
                            Console.WriteLine("The total cost of your items is : " + s.Checkout());

                            break;

                        default:
                            break;
                    }


                    action = chooseAction();

                }


            }

            else

                Console.WriteLine("Console Active! Please Solution Explorer, click on the Form1.cs or what ever the Form.cs you are using, choose Properties (or press Alt + Enter) and go to Output type: choos Windows Application ( not the Console Appplication)" );

        }




        private static void printShoppingCart(Store s)
        {
            Console.WriteLine("Cars you have chosen to buy ");
            for (int i = 0; i < s.ShoppingList.Count; i++)
            {
                Console.WriteLine("ShoppingList #: " + i + " " + s.ShoppingList[i]);
            }
        }

        private static void printInventory(Store s)
        {
            for (int i = 0; i < s.CarList.Count; i++)
            {
                Console.WriteLine("CarList #: " + i + " " + s.CarList[i]);

            }
        }

        static public int chooseAction()
        {
            int choice = 0;
            Console.WriteLine("Choose an action (0) to quit (1) to add a new car to inventory (2) add car to cart (3) checkout ");

            choice = int.Parse(Console.ReadLine());
            return choice;
        }




        //=========================================================================================================================
        //Car App Classes 

        public class Car
        {
            public string Make { get; set; }
            public string Model { get; set; }
            public decimal Price { get; set; }



            public Car()
            {
                Make = "nothing yet ";
                Model = "nothing yet "; ;
                Price = 0.00M;
            }


            public Car(string a, string b, decimal c)
            {
                Make = a;
                Model = b;
                Price = c;
            }


            override public string ToString()
            {
                return " Make: " + Make + " Model " + Model + " Price: $ " + Price;
            }

        }


        public class Store
        {
            public List<Car> CarList { get; set; }
            public List<Car> ShoppingList { get; set; }

            public Store()
            {
                CarList = new List<Car>();
                ShoppingList = new List<Car>();
            }

            public decimal Checkout()
            {
                // initialize the total cost
                decimal totalCost = 0;

                foreach (var c in ShoppingList)
                {
                    totalCost = totalCost + c.Price;
                }
                ShoppingList.Clear();
                return totalCost;
            }

        }

        //=========================================================================================================================




    }

}