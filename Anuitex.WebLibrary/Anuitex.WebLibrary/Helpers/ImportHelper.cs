using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;
using Anuitex.WebLibrary.Data.Models;

namespace Anuitex.WebLibrary.Helpers
{
    public static class ImportHelper
    {
        public static List<BookModel> ImportBooks(Stream stream, bool isXml)
        {
            if (isXml)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<BookModel>));
                return (List<BookModel>)serializer.Deserialize(stream);
            }
            else
            {
                List<BookModel> result = new List<BookModel>();
                using (StreamReader streamReader = new StreamReader(stream, Encoding.Default))
                {
                    string[] data = streamReader.ReadToEnd().Replace("\r", "").Split('\n');

                    if (data[0] != "Books") { throw new Exception("Incorrect file");}

                    for (var i = 0; i + 8 < data.Length; i += 10)
                    {
                        result.Add(new BookModel()
                        {
                            Id = int.Parse(data[i + 1]),
                            Title = data[i + 2],
                            Year = int.Parse(data[i + 3]),
                            Pages = int.Parse(data[i + 4]),
                            Author = data[i + 5],
                            Genre = data[i + 6],
                            Amount = int.Parse(data[i + 7]),
                            Price = double.Parse(data[i + 8]),
                            PhotoId = int.Parse(data[i + 9]),
                            PhotoPath = data[i + 10]
                        });
                    }
                }
                return result;
            }
        }

        public static List<JournalModel> ImportJournals(Stream stream, bool isXml)
        {
            if (isXml)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<JournalModel>));
                return (List<JournalModel>)serializer.Deserialize(stream);
            }
            else
            {
                List<JournalModel> result = new List<JournalModel>();
                using (StreamReader streamReader = new StreamReader(stream, Encoding.Default))
                {
                    string[] data = streamReader.ReadToEnd().Replace("\r", "").Split('\n');

                    if (data[0] != "Journals") { throw new Exception("Incorrect file");}

                    for (var i = 0; i + 8 < data.Length; i += 9)
                    {
                        result.Add(new JournalModel()
                        {
                            Id = int.Parse(data[i + 1]),
                            Title = data[i + 2],
                            Subjects = data[i + 3],
                            Periodicity = data[i + 4],
                            Date = data[i + 5],                            
                            Amount = int.Parse(data[i + 6]),
                            Price = double.Parse(data[i + 7]),
                            PhotoId = int.Parse(data[i + 8]),
                            PhotoPath = data[i + 9]
                        });
                    }
                }
                return result;
            }
        }

        public static List<NewspaperModel> ImportNewspapers(Stream stream, bool isXml)
        {
            if (isXml)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<NewspaperModel>));
                return (List<NewspaperModel>)serializer.Deserialize(stream);
            }
            else
            {
                List<NewspaperModel> result = new List<NewspaperModel>();
                using (StreamReader streamReader = new StreamReader(stream, Encoding.Default))
                {
                    string[] data = streamReader.ReadToEnd().Replace("\r", "").Split('\n');

                    if (data[0] != "Newspapers") { throw new Exception("Incorrect file");}

                    for (var i = 0; i + 8 < data.Length; i += 8)
                    {
                        result.Add(new NewspaperModel()
                        {
                            Id = int.Parse(data[i + 1]),
                            Title = data[i + 2],                            
                            Periodicity = data[i + 3],
                            Date = data[i + 4],
                            Amount = int.Parse(data[i + 5]),
                            Price = double.Parse(data[i + 6]),
                            PhotoId = int.Parse(data[i + 7]),
                            PhotoPath = data[i + 8]
                        });
                    }
                }
                return result;
            }
        }
    }
}