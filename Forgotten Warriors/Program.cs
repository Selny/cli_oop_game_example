using System;
using System.Collections.Generic;

namespace ForgottenWarriors {

    class Game {

        private Character c1 = null!;
        private Character c2 = null!;
        public int GameMode;
        public int turn = 0;
        public List<int> logs = new List<int>(){0};

        public override string ToString () => $"{c1}\t{c2}\nHP: {c1.hp}\t\t\tHP: {c2.hp}";

        public void Start() {

            Console.Title = "Forgotten Warriors";
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Choose game mode:\n1) PvP\n2) Player vs Bot\n3) Bot vs Bot");
            GameMode = int.Parse(Console.ReadKey(true).KeyChar.ToString());
            Console.Clear();
            
            Character[] characters1 = { 
                new SadKnight("Neutral Leo"), 
                new Pharmacist("Neutral Skaia"),
                new Wanderer("Neutral Leah"),
                new Viper("Neutral Toxic")
            };

            Character[] characters2 = { 
                new SadKnight("Dark Leo"), 
                new Pharmacist("Dark Skaia"),
                new Wanderer("Dark Leah"),
                new Viper("Dark Toxic")
            };
            
            int p1, p2;
            System.Console.WriteLine("1)SadKnight\n2)Pharmacist\n3)Wanderer\n4)Viper\n");

            if (GameMode == 1) {
                System.Console.Write("Choose player 1: ");
                p1 = int.Parse(Console.ReadKey(true).KeyChar.ToString());
                System.Console.WriteLine($" {characters1[p1-1]}");
                System.Console.Write("Choose player 2: ");
                p2 = int.Parse(Console.ReadKey(true).KeyChar.ToString());
                System.Console.WriteLine($" characters2[p2-1]");
                Console.Clear();
            }

            else if (GameMode == 2) {
                System.Console.Write("Choose player 1: ");
                p1 = int.Parse(Console.ReadKey(true).KeyChar.ToString());
                System.Console.WriteLine($" {characters1[p1-1]}");
                System.Console.Write("Choose player 2: ");
                Random r = new Random();
                p2 = r.Next(1, 5);
                System.Console.WriteLine($" characters2[p2-1]");
                Console.Clear();
            }
            else {
                Random r = new Random();
                System.Console.Write("Choose player 1: ");
                p1 = r.Next(1, 5);
                System.Console.WriteLine($" {characters1[p1-1]}");
                System.Console.Write("Choose player 2: ");
                p2 = r.Next(1, 5);
                System.Console.WriteLine($" characters2[p2-1]");
                Console.Clear();
            }


            this.c1 = characters1[p1-1];
            this.c2 = characters2[p2-1];

        }

        public void Input(ref Game game) {
            if (turn % 2 == 0) {
                logs.Add(c1.Input(ref game));
                c2.hp -= logs[^1];
                System.Console.WriteLine($"Attack: {logs[^1]}");
            }
            else {
                logs.Add(c2.Input(ref game));
                c1.hp -= logs[^1];
                System.Console.WriteLine($"Attack: {logs[^1]}");
            }
            System.Threading.Thread.Sleep(2000);
            turn++;
        }
        public bool isRunning() {
            if (c1.hp <= 0) {
                System.Console.WriteLine($"{c2} won the battle!");
            }
            else if (c2.hp <= 0) {
                System.Console.WriteLine($"{c1} won the battle!");
            }

            return c1.hp > 0 && c2.hp > 0;
        }

        public void show_logs() {
            int i = 1;
            foreach(var damage in logs) {
                if (i % 2 == 0) System.Console.WriteLine(c1);
                else System.Console.WriteLine(c2);
                if (i!=0)System.Console.WriteLine(damage);
                i++;
            }
        }

    }

    abstract class Character {

        protected string name = "";
        public int hp;
        protected int damage;
        // protected int mana;

        public abstract int Input(ref Game g);
        public virtual int Attack() {
            System.Console.WriteLine($"Damage: {damage}");
            return damage;
        }

    }

    class SadKnight : Character {

        private int maxDamage = 40;
        
        public SadKnight(string name) {
            this.name = name;
            hp = 75;
            damage = 30;
        }

        public override string ToString() => $"{name} the Sad Knight";

        public override int Attack() {
            Random r = new Random();
            int finalDamage = r.Next(damage, maxDamage);
            System.Console.WriteLine($"Damage: {finalDamage}");
            return finalDamage;
        }
        public int CryAttack() {
            System.Console.WriteLine(@"Self damage -20. Damage +50");
            hp -= 20;
            return 60;
        }
        public override int Input(ref Game game) {
            System.Console.WriteLine("\nChoose a skill:\nQ > Attack\nW > CryAttack");
            ConsoleKey option;
            if ((game.GameMode == 2 && game.turn % 2 == 1) || game.GameMode == 3) {
                Random r = new Random();
                if (r.Next(0, 1) == 0) option = ConsoleKey.Q;
                else option = ConsoleKey.W;
            }
            else option = Console.ReadKey(true).Key;
            if (option == ConsoleKey.Q) return Attack();
            else if (option == ConsoleKey.W) return CryAttack();
            else {
                System.Console.WriteLine("Invalid option");
                return Input(ref game);
            }
        }

    }

    class Pharmacist : Character {

        public Pharmacist(string name) {
            this.name = name;
            hp = 60;
            damage = 17;
        }

        public override string ToString() => $"{name} the Pharmacist";

        public int Happyness() {
            System.Console.WriteLine("HP +20");
            hp += 15;
            return 0;
        }

        public override int Input(ref Game game) {
            System.Console.WriteLine("\nChoose a skill:\nQ > Attack\nW > Happyness");
            ConsoleKey option;
            if ((game.GameMode == 2 && game.turn % 2 == 1) || game.GameMode == 3) {
                Random r = new Random();
                if (r.Next(0, 1) == 0) option = ConsoleKey.Q;
                else option = ConsoleKey.W;
            }
            else option = Console.ReadKey(true).Key;
            if (option == ConsoleKey.Q) return Attack();
            else if (option == ConsoleKey.W) return Happyness();
            else {
                return Input(ref game);
            }
        }
    }

    class Wanderer : Character {

        public Wanderer(string name) {
            this.name = name;
            hp = 65;
            damage = 20;
        }

        public override string ToString() => $"{name} the Wanderer";

        public int ReverseAttack(ref Game g) {
            System.Console.WriteLine($"Reverse Attack!\nDamage: {g.logs[^1]/2}");
            hp += g.logs[^1] / 2;
            return g.logs[^1] / 2;
        }

        public override int Input(ref Game game) {
            System.Console.WriteLine("\nChoose a skill:\nQ > Attack\nW > Reverse Attack");
            ConsoleKey option;
            if ((game.GameMode == 2 && game.turn % 2 == 1) || game.GameMode == 3) {
                Random r = new Random();
                if (r.Next(0, 1) == 0) option = ConsoleKey.Q;
                else option = ConsoleKey.W;
            }
            else option = Console.ReadKey(true).Key;
            if (option == ConsoleKey.Q) return Attack();
            else if (option == ConsoleKey.W) return ReverseAttack(ref game);
            else {
                return Input(ref game);
            }
        }
        

    }

    class Viper : Character {

        private int poisionDamage = 0;
        private int duration = 3;
        private bool active = false;

        public Viper(string name) {
            this.name = name;
            damage = 17;
            hp = 73;
        }

        public override string ToString() => $"{name} the Viper";

        public int PoisionAttack(){
            System.Console.WriteLine($"Poision Attack!\nDamage{poisionDamage}\nDuration: 3 rounds");
            active = true;
            return 15;
        }

        public override int Input(ref Game game) {
            System.Console.WriteLine("\nChoose a skill:\nQ > Attack\nW > Poision Attack");
            ConsoleKey option;
            if ((game.GameMode == 2 && game.turn % 2 == 1) || game.GameMode == 3) {
                Random r = new Random();
                if (r.Next(0, 1) == 0) option = ConsoleKey.Q;
                else option = ConsoleKey.W;
            }
            else option = Console.ReadKey(true).Key;
            if (active) {
                poisionDamage = 10;
                duration--;
            }
            if (duration == 0) {
                poisionDamage = 0;
                active = false;
                duration = 3;
            }
            if (option == ConsoleKey.Q) return Attack() + poisionDamage;
            else if (option == ConsoleKey.W) return PoisionAttack();
            else {
                return Input(ref game);
            }
        }


    }

    internal class Program {

        public static void Main(String[] args) {

            Game game = new();

            game.Start();

            while (game.isRunning()) {
                System.Console.WriteLine(game);
                game.Input(ref game);
                Console.Clear();
            }

        }

    }
    
}
