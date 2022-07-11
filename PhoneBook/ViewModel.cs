using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PhoneBook
{
    class ViewModel
    {
        //PS: Подобие VM. Visual.StateManager пока что не реализован. 
        private List<Contact> contacts;
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            //Debug
            contacts = new List<Contact>()
            {
                new Contact() { Name="Andrey", Number=7999999999 },
                new Contact() { Name ="Mama", Number=09090909090},
                new Contact() { Name="Mama", Number=1111111 },
                new Contact() { Name="AndMama", Number=1111111}
            };
        }

        public List<Contact> this[string name]
        {
            get
            {
                if(contacts.Count != 0 && contacts != null)
                {
                    List<Contact> selectedContacts = contacts.FindAll(nam => nam.Name == name);
                    return selectedContacts;
                }
                return null;
            }
        }

        public List<Contact> this[long phone]
        {
            get
            {
                if(contacts != null && contacts.Count != 0)
                {
                    List<Contact> selectedContacts = contacts.FindAll(ph => ph.Number == phone);
                    return selectedContacts;
                }
                return null;
            }
        }
    }
}
