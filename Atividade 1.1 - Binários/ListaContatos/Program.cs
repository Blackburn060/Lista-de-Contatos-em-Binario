using System;
using System.Collections.Generic;
using System.IO;

class Contact
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}

class Program
{
    static List<Contact> contacts = new List<Contact>();
    static string dataFilePath = "Contatos.bin";

    static void Main(string[] args)
    {
        LoadContacts();

        while (true)
        {
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("1 - Adicionar um contato");
            Console.WriteLine("2 - Listar contatos");
            Console.WriteLine("3 - Sair");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddContact();
                    break;
                case "2":
                    ListContacts();
                    break;
                case "3":
                    SaveContacts();
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    static void AddContact()
    {
        Contact contact = new Contact();

        Console.Write("Nome: ");
        contact.Name = Console.ReadLine();

        Console.Write("Número de telefone: ");
        contact.PhoneNumber = Console.ReadLine();

        Console.Write("E-mail: ");
        contact.Email = Console.ReadLine();

        contacts.Add(contact);
        Console.WriteLine("Contato adicionado com sucesso!");
    }

    static void ListContacts()
    {
        Console.WriteLine("Lista de Contatos:");
        foreach (var contact in contacts)
        {
            Console.WriteLine($"Nome: {contact.Name}");
            Console.WriteLine($"Telefone: {contact.PhoneNumber}");
            Console.WriteLine($"E-mail: {contact.Email}");
            Console.WriteLine();
        }
    }

    static void SaveContacts()
    {
        try
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(dataFilePath, FileMode.Create)))
            {
                foreach (var contact in contacts)
                {
                    writer.Write(contact.Name);
                    writer.Write(contact.PhoneNumber);
                    writer.Write(contact.Email);
                }
            }
            Console.WriteLine("Contatos salvos com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar contatos: {ex.Message}");
        }
    }

    static void LoadContacts()
    {
        if (File.Exists(dataFilePath))
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(dataFilePath, FileMode.Open)))
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        Contact contact = new Contact
                        {
                            Name = reader.ReadString(),
                            PhoneNumber = reader.ReadString(),
                            Email = reader.ReadString()
                        };
                        contacts.Add(contact);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar contatos: {ex.Message}");
            }
        }
    }
}