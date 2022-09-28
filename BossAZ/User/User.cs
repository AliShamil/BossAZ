using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
enum Azerbaijan
{
    Agdash,
    Aghjabadi,
    Agstafa,
    Agsu,
    Astara,
    Aghdara,
    Babek,
    Baku,
    Balakən,
    Barda,
    Beylagan,
    Bilasuvar,
    Dashkasan,
    Shabran,
    Fuzuli,
    Gadabay,
    Ganja,
    Goranboy,
    Goychay,
    Goygol,
    Hajigabul,
    Imishli,
    Ismayilli,
    Jabrayil,
    Julfa,
    Kalbajar,
    Khachmaz,
    Khankendi,
    Khojavend,
    Khirdalan,
    Kurdamir,
    Lankaran,
    Lerik,
    Masally,
    Mingachevir,
    Nakhchivan,
    Naftalan,
    Neftchala,
    Oghuz,
    Ordubad,
    Qabala,
    Qakh,
    Qazakh,
    Quba,
    Qubadli,
    Qusar,
    Saatlı,
    Sabirabad,
    Shahbuz,
    Shaki,
    Shamakhi,
    Shamkir,
    Sharur,
    Shirvan,
    Siyazan,
    Shusha,
    Sumgait,
    Tartar,
    Tovuz,
    Ujar,
    Yardimli,
    Yevlakh,
    Zaqatala,
    Zardab,
    Zangilan,
}
namespace BossAZ.User
{
    abstract class User
    {
        public Guid Id { get;  }

        private bool ValidateName(string? name) => Regex.IsMatch(name!, @"^[a-zA-Z]+$");
        private bool ValidatePhone(string? phone) => Regex.IsMatch(phone!, @"^([0|\+[0-9]{1,5})?([0-9]{10})$");
        private string? name;
        private string? surname;
        private string? phone;
        private byte age;


        public string? Name
        {
            get { return name; }
            set
            {
                if (!ValidateName(value) || value?.Length < 3)
                    throw new ArgumentException("Invalid Name");

                name = value;
            }
        }


        public string? Surname
        {
            get { return surname; }
            set
            {
                if (!ValidateName(value) || value?.Length < 3)
                    throw new ArgumentException("Invalid Name!");

                surname = value;
            }
        }

        public Azerbaijan City { get; set; }


        public string? Phone
        {
            get { return phone; }
            set
            {
                if (!ValidatePhone(value))
                    throw new ArgumentException("Invalid Phone!");

                phone = value;
            }
        }


        public byte Age
        {
            get { return age; }
            set
            {
                if (value < 18)
                    throw new ArgumentException("Your age is not enough !");

                if (value > 70)
                    throw new ArgumentException("You are useless for working !");

                age = value;
            }
        }


        protected User(string? name, string? surname, Azerbaijan city, string? phone, byte age)
        {
            Id = Guid.NewGuid();
            Name=name;
            Surname=surname;
            City=city;
            Phone=phone;
            Age=age;
        }

        public override string ToString()
            => $@"Name: {Name}
Surname: {Surname}
City: {City}
Phone: {Phone}
Age: {Age}";
    }
}
