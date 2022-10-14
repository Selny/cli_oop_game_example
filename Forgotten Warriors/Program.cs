using System;

namespace ForgottenWarriors {

    class Game {

        private Character c1;
        private Character c2;
        private int turn = 0;

        public Game(string title, ref Character c1, ref Character c2) {
            Console.Title = title;
            //Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            this.c1 = c1;
            this.c2 = c2;
        }

        public override string ToString () => $"{c1}\t{c2}\nHP: {c1.hp}\t\t\tHP: {c2.hp}";
        public void Input() {
            int od;
            if (turn % 2 == 0) {
                od = c1.Input();
                c2.hp -= od;
            }
            else {
                od = c2.Input();
                c1.hp -= od;
            }
            turn++;
        }
        public bool Running() {
            if (c1.hp <= 0) {
                System.Console.WriteLine($"{c2} won the battle!");
            }
            else if (c2.hp <= 0) {
                System.Console.WriteLine($"{c1} won the battle!");
            }
            return c1.hp > 0 && c2.hp > 0;
        }

    }

    abstract class Character {

        protected string name = "";
        public int hp;
        protected int damage;
        // protected int mana;

        public abstract int Input();
        public abstract int Attack();

    }

    class SadKnight : Character {

        private int maxDamage = 60;
        
        public SadKnight(string name) {
            this.name = name;
            hp = 50;
            damage = 20;
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
            return 50;
        }
        public override int Input() {
            System.Console.WriteLine("\nChoose a skill:\nQ > Attack\nW > CryAttack");
            var option = Console.ReadKey(true).Key;
            if (option == ConsoleKey.Q) return Attack();
            else if (option == ConsoleKey.W) return CryAttack();
            else {
                System.Console.WriteLine("Invalid option");
                return Input();
            }
        }

    }

    class Pharmacist : Character {

        public Pharmacist(string name) {
            this.name = name;
            hp = 80;
            damage = 30;
        }

        public override string ToString() => $"{name} the Pharmacist";

        public override int Attack() {
            System.Console.WriteLine($"Damage: {damage}");
            return damage;
        }
        public int Happyness() {
            System.Console.WriteLine("HP +20");
            hp += 20;
            return 0;
        }

        public override int Input() {
            System.Console.WriteLine("Choose a skill:\nQ > Attack\nW > Happyness");
            var option = Console.ReadKey(true).Key;
            if (option == ConsoleKey.Q) return Attack();
            else if (option == ConsoleKey.W) return Happyness();
            else {
                return Input();
            }
        }
    }


    internal class Program {

        public static void Main(String[] args) {

            Character sk = new SadKnight("Leo");
            Character p = new Pharmacist("Skaia");

            Game game = new Game("Forgotten Warriors", ref sk, ref p);

            while (game.Running()) {
                System.Console.WriteLine(game);
                game.Input();
                Console.Clear();
            }

        }

    }
    
}