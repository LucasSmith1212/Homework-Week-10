using System;

namespace Project_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Duelist Aaron = new Duelist("Aaron", 1.0 / 3.0);
            Duelist Bob = new Duelist("Bob", 0.5);
            Duelist Charlie = new Duelist("Charlie", 0.995);

            double aaronWin = 0;
            double bobWin = 0;
            double charlieWin = 0;

            int shot = 1;
            int repeat = 10000;
            int counter = 1;

            for (int index = 0; index < repeat; index++)
            {
                while (Duelist.getDeaths() < 2)
                {
                    if (Aaron.isAlive() && (Duelist.getDeaths() < 2) && shot != 1)
                    {
                        Aaron.intentionalMiss(counter);
                        if (Charlie.isAlive())
                        {
                            Aaron.shootAtTarget(Charlie);
                        }
                        else
                        {
                            Aaron.shootAtTarget(Bob);
                        }
                    }
                    if (Bob.isAlive() && (Duelist.getDeaths() < 2))
                    {
                        if (Charlie.isAlive())
                        {
                            Bob.shootAtTarget(Charlie);
                        }
                        else
                        {
                            Bob.shootAtTarget(Aaron);
                        }
                    }
                    if (Charlie.isAlive() && (Duelist.getDeaths() < 2))
                    {
                        if (Bob.isAlive())
                        {
                            Charlie.shootAtTarget(Bob);
                        }
                        else
                        {
                            Charlie.shootAtTarget(Aaron);
                        }
                    }
                    counter++;
                    shot++;
                }

                if (Aaron.isAlive())
                {
                    aaronWin++;
                }
                else if (Bob.isAlive())
                {
                    bobWin++;
                }
                else
                {
                    charlieWin++;
                }

                Duelist.resetDeaths();
                shot = 1;
                counter = 1;
                Aaron.resurrect();
                Bob.resurrect();
                Charlie.resurrect();
            }

            double aaronRate = (aaronWin / 10000);
            double bobRate = (bobWin / 10000);
            double charlieRate = (charlieWin / 10000);

            Console.WriteLine(Aaron.toString());
            Console.WriteLine(Bob.toString());
            Console.WriteLine(Charlie.toString());
            Console.WriteLine("Aaron won " + (aaronRate * 100) + "% of the time.");
            Console.WriteLine("Bob won " + (bobRate * 100) + "% of the time.");
            Console.WriteLine("Charlie won " + (charlieRate * 100) + "% of the time.");
        }
    }

    public class Duelist
    {
        private string name;
        private double accuracy;
        private bool alive;
        private static int deaths;



        public Duelist(string initialName, double initialAcc)
        {
            name = initialName;
            accuracy = initialAcc;
            alive = true;
        }

        public Duelist(Duelist otherDuelist)
        {
            name = otherDuelist.name;
            accuracy = otherDuelist.getAccuracy();
            alive = otherDuelist.isAlive();
        }

        public string getName()
        {
            return name;
        }

        public double getAccuracy()
        {
            return accuracy;
        }

        public bool isAlive()
        {
            return alive;
        }

        public void setName(string name)
        {
            name = this.name;
        }

        public void setAccuracy(double accuracy)
        {
            accuracy = this.accuracy;
        }

        public void set(string newName, double newAccuracy)
        {
            name = newName;
            accuracy = newAccuracy;
        }

        public static int getDeaths()
        {
            return deaths;
        }

        public void shootAtTarget(Duelist target)
        {
            Random rand = new Random();
            int rInt = rand.Next(0, 100);
            if ((double)rInt < accuracy)
            {
                target.kill();
            }
        }

        private void kill()
        {
            alive = false;
            deaths++;
        }

        public bool equals(Duelist otherDuelist)
        {
            return ((name.Equals(otherDuelist.name)) && (accuracy == otherDuelist.getAccuracy()));
        }

        public static void resetDeaths()
        {
            deaths = 0;
        }

        public void resurrect()
        {
            alive = true;
        }

        public void intentionalMiss(int shot)
        {
            if (shot == 1)
            {
                accuracy = 0;
            }
        }

        public string toString()
        {
            if (alive)
            {
                return (name + ", " + (accuracy * 100) + "% accuracy.");
            }
            else
            {
                return (name + ", " + (accuracy * 100) + "% accuracy.");
            }
        }
    }
}
