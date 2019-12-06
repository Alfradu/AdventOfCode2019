using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    class Day6
    {
        public struct Orbit
        {
            public string planet;
            public List<string> children;

            public void addChild(string child)
            {
                children.Add(child);
            }

            public string getChild(string child)
            {
                return children.Find(o => o.Equals(child));
            }

            public int getChildCount()
            {
                return children.Count;
            }

            public override string ToString()
            {
                return planet + " ) " + string.Join(", ", children.ToArray());
            }
        }

        int counter;
        List<Orbit> orbits;
        public void Puzzle()
        {
            counter = 0;
            string[] input = System.IO.File.ReadAllText(@"K:\Android projects\AdventOfCode2019\input6.txt").Split('\n');
            //string[] input = System.IO.File.ReadAllText(@"K:\Android projects\AdventOfCode2019\test.txt").Split('\n');
            orbits = createOrbits(input);
            addChildOrbits(input);
            Orbit center = orbits.Find(o => o.planet == "COM");
            Orbit you = orbits.Find(o => o.planet == "YOU");
            Orbit santa = orbits.Find(o => o.planet == "SAN");
            List<Orbit> youBranch = findBranch(you);
            List<Orbit> santaBranch = findBranch(santa);
            List<Orbit> queueDelete = new List<Orbit>();

            findNext(center, 0);
            Console.WriteLine(counter);

            foreach(Orbit o in youBranch)
            {
                foreach(Orbit or in santaBranch)
                {
                    if (o.Equals(or))
                    {
                        queueDelete.Add(o);
                    }
                }
            }
            foreach(Orbit o in queueDelete)
            {
                youBranch.Remove(o);
                santaBranch.Remove(o);
            }
            Console.WriteLine("distance to santaa: " + (youBranch.Count + santaBranch.Count));
        }

        public void findNext(Orbit o, int i)
        {
            counter += i;
            Console.WriteLine(String.Format("{0,-20}{1,-5}{2,5}", o,i,counter));
            List<Orbit> ol = new List<Orbit>();
            foreach(string c in o.children)
            {
                ol.Add(orbits.Find(o => o.planet == c));
            }
            foreach(Orbit or in ol)
            {
                findNext(or, i+1);
            }
        }

        public List<Orbit> findBranch(Orbit o)
        {
            List<Orbit> ol = new List<Orbit>();
            Orbit currentPlanet = o;
            while(currentPlanet.planet != "COM")
            {
                currentPlanet = orbits.Find(o => o.getChild(currentPlanet.planet) != null);
                ol.Add(currentPlanet);
            }
            return ol;
        }
        
        public List<Orbit> createOrbits(string[] input)
        {
            List<Orbit> orbits = new List<Orbit>();
            foreach (string i in input)
            {
                Orbit newO = new Orbit
                {
                    planet = i.Substring(0, 3),
                    children = new List<string>()
                };
                newO.addChild(i.Substring(4, 3));
                orbits.Add(newO);

                foreach (Orbit o in orbits)
                {
                    if (o.planet == i.Substring(0, 3))
                    {
                        if (o.getChild(i.Substring(4, 3)) == null)
                        {
                            o.addChild(i.Substring(4, 3));
                        }
                    }
                }
            }
            return orbits;
        }

        public void addChildOrbits(string[] input)
        {
            List<Orbit> childOrbits = new List<Orbit>();
            foreach (string s in input)
            {
                if (orbits.FindAll(o => o.planet == s.Substring(4, 3)).Count == 0)
                {
                    childOrbits.Add(new Orbit
                    {
                        planet = s.Substring(4, 3),
                        children = new List<string>()
                    });
                }
            }
            orbits.AddRange(childOrbits);
        }
    }
}
