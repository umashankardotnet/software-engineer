using System;
using System.Collections.Generic;

namespace csharp_practice
{
    public class ObjectEqualityTest
    {
        public static void Main()
        {
            // Create a Person object for each job applicant.
            Person applicant1 = new Person("Jones", "099-29-4999");
            Person applicant2 = new Person("Jones", "199-29-3999");
            Person applicant3 = new Person("Jones", "299-49-6999");

            // Add applicants to a List object.
            List<Person> applicants = new List<Person>();
            applicants.Add(applicant1);
            applicants.Add(applicant2);
            applicants.Add(applicant3);

            // Create a Person object for the final candidate.
            Person candidate = new Person("Jones", "199-29-3999");

            // call Iquitable mwthod
            Console.WriteLine(Person.Equals(applicant2, candidate));

            if (applicants.Contains(candidate))
                Console.WriteLine("Found {0} (SSN {1}).",
                                   candidate.LastName, candidate.SSN);
            else
                Console.WriteLine("Applicant {0} not found.", candidate.SSN);

            // Call the shared inherited Equals(Object, Object) method.
            // It will in turn call the IEquatable(Of T).Equals implementation.
            Console.WriteLine("{0}({1}) already on file: {2}.",
                              applicant2.LastName,
                              applicant2.SSN,
                              Person.Equals(applicant2, candidate));

            Console.ReadLine();
        }
    }



    public class Person : IEquatable<Person>
    {
        private string uniqueSsn;
        private string lName;

        public Person(string lastName, string ssn)
        {
            uniqueSsn = ssn;

            this.LastName = lastName;
        }

        public string SSN
        {
            get { return this.uniqueSsn; }
        }

        public string LastName
        {
            get { return this.lName; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("The last name cannot be null or empty.");
                else
                    this.lName = value;
            }
        }

        public bool Equals(Person other)
        {
            if (other == null)
                return false;

            if (this.uniqueSsn == other.uniqueSsn)
                return true;
            else
                return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Person personObj = obj as Person;
            if (personObj == null)
                return false;
            else
                return Equals(personObj);
        }

        public override int GetHashCode()
        {
            return this.SSN.GetHashCode();
        }

        public static bool operator ==(Person person1, Person person2)
        {
            if (((object)person1) == null || ((object)person2) == null)
                return Object.Equals(person1, person2);

            return person1.Equals(person2);
        }

        public static bool operator !=(Person person1, Person person2)
        {
            if (((object)person1) == null || ((object)person2) == null)
                return !Object.Equals(person1, person2);

            return !(person1.Equals(person2));
        }
    }
}
