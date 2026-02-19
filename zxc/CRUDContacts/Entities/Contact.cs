using System;

namespace CRUDContacts.Entities
{
    public class Contact : IComparable
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime CreateTime { get; set; }

        public string PhoneNumber
        {
            get => Phone;
            set => Phone = value;
        }


        public Contact(string name, string phone)
        {
            Name = name;
            Phone = phone;
            CreateTime = DateTime.Now;
        }

        public Contact(string name, string phone, DateTime createTime)
        {
            Name = name;
            Phone = phone;
            CreateTime = createTime;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            if (obj is not Contact other)
                throw new ArgumentException("Object is not Contact");

            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }

        public bool MatchesQuery(string query)
        {
            return Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                   Phone.Contains(query, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return $"{Name};{Phone}";
        }
    }
}
