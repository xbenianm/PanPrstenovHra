using System;
using System.Collections.Generic;
using System.IO;
using System.Media;


namespace PANPRSTENOVHRA
{
    internal class Hrac
    {
        public string meno;
        public string rasa;
        public int zdravie = 10;
        public int poskodenie = 2;
        public int stit = 1;
        public int lektvar = 5;
        public string Tvar = "( O  \r\n /|\\  \r\n / \\ )";
    }

    internal class Nepriateľ
    {
        public string meno;
        public int zdravie = 12;
        public int poskodenie = 4;
        public string Tvar = "( ,     ,\r\n       /(     )\\  \r\n      //\\___/\\\\  \r\n      \\\\_🔥_//  \r\n    ( /O O\\ )  \r\n     \\  ~  /  \r\n      \\ - /  \r\n     /`---'\\  \r\n    /       \\  \r\n   / |  🛡  | \\  \r\n  *  |  ⚔  |  *  \r\n     |     |  \r\n     ^^^^^^^  )";
    }

    internal class Program
    {
        public static Hrac currentHrac = new Hrac();
        public static Nepriateľ nepriatel;
        static Random random = new Random();

        static void Main(string[] args)
        {
            StartHry();
            Boj();
        }

        static void StartHry()
        {
            Console.WriteLine("Vitaj vo svete Pána Prsteňov!");
            PlaySound("PanPrstenov.wav");
            Console.Write("Zadaj svoje meno: ");
            currentHrac.meno = Console.ReadLine();

            Console.Write("Vyber si rasu (Človek, Elf, Trpaslík, Škriatok): ");
            currentHrac.rasa = Console.ReadLine();

            string[] nepriatelia = { "Melkor", "Sauron", "Saruman" };
            nepriatel = new Nepriateľ { meno = nepriatelia[random.Next(nepriatelia.Length)] };
            PlaySound("Iseeyou.wav");

            Console.WriteLine($"Tvoj nepriateľ je {nepriatel.meno}!");
            Console.WriteLine("Stlač ľubovoľnú klávesu...");
            Console.ReadKey();
            Console.Clear();
        }

        static void Boj()
        {
            while (currentHrac.zdravie > 0 && nepriatel.zdravie > 0)
            {
                Console.WriteLine("==============================");
                Console.WriteLine($"{currentHrac.Tvar} {currentHrac.meno} ({currentHrac.rasa})");
                Console.WriteLine($"Zdravie: {currentHrac.zdravie} | Obrana: {currentHrac.stit}");
                Console.WriteLine($"{nepriatel.Tvar} {nepriatel.meno} (Zdravie: {nepriatel.zdravie})");
                Console.WriteLine("==============================");
                Console.WriteLine("..U(utok) O(obrana)..");
                Console.WriteLine("..B(bez) L(liečsa)..");
                Console.WriteLine("==============================");
                Console.Write("Tvoj ťah: ");
                string volba = Console.ReadLine().ToUpper();
                Console.Clear();

                switch (volba)
                {
                    case "U":
                        PlaySound(800, 200);
                        PlaySound("attack.wav"); // Prehrá zvuk útoku
                        Utok();
                        break;
                    case "O":
                        PlaySound(600, 300);
                        Obrana();
                        break;
                    case "B":
                        PlaySound(1000, 200);
                        if (random.Next(0, 2) == 1)
                        {
                            Console.WriteLine("Úspešne si utiekol!");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Neúspešný útek!");
                        }
                        break;
                    case "L":
                        PlaySound(500, 300);
                        Liecenie();
                        break;
                    default:
                        Console.WriteLine("Neplatná voľba!");
                        break;
                }

                if (nepriatel.zdravie > 0)
                    NepriatelUtok();
            }

            Console.WriteLine(currentHrac.zdravie > 0 ? "Vyhral si boj!" : "Prehral si!");
            Console.ReadKey();
        }

        static void PlaySound(int frequency, int duration)
        {
            Console.Beep(frequency, duration);
        }

        static void Utok()
        {
            nepriatel.zdravie -= currentHrac.poskodenie;
            Console.WriteLine($"Útočíš! Spôsobil si {currentHrac.poskodenie} poškodenia.");
            ZobrazStatus();
        }

        static void Obrana()
        {
            currentHrac.stit += 1;
            Console.WriteLine("Brániš sa! Obrana sa zvýšila.");
            ZobrazStatus();
        }

        static void Liecenie()
        {
            if (currentHrac.lektvar > 0)
            {
                currentHrac.zdravie += 5;
                currentHrac.lektvar--;
                Console.WriteLine("Vypil si liečivý lektvar!");
            }
            else
            {
                Console.WriteLine("Nemáš viac lektvarov!");
            }
            ZobrazStatus();
        }

        static void NepriatelUtok()
        {
            PlaySound(700, 200);
            int skutocnePoskodenie = nepriatel.poskodenie - currentHrac.stit;
            if (skutocnePoskodenie < 0) skutocnePoskodenie = 0;

            currentHrac.zdravie -= skutocnePoskodenie;
            Console.WriteLine($"{nepriatel.meno} útočí! Spôsobil ti {skutocnePoskodenie} poškodenia.");
            ZobrazStatus();
        }

        static void ZobrazStatus()
        {
            Console.WriteLine($"Tvoj aktuálny stav -> Zdravie: {currentHrac.zdravie} | Obrana: {currentHrac.stit}");
            Console.WriteLine("Stlač ľubovoľnú klávesu...");
            Console.ReadKey();
            Console.Clear();
        }
        static void PlaySound(string soundFileName)
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", soundFileName);
                if (File.Exists(path))
                {
                    using (SoundPlayer player = new SoundPlayer(path))
                    {
                        player.PlaySync(); // Prehrá synchronne
                    }
                }
                else
                {
                    Console.WriteLine($"Súbor {soundFileName} neexistuje na ceste {path}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba pri prehrávaní zvuku: {ex.Message}");
            }
        }
    }
    }





