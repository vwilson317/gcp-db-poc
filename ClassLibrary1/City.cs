using System;
using Google.Cloud.Firestore;

namespace ClassLibrary1
{
    [FirestoreData]
    public class City : FirestoreCollection
    {
        public City()
        {
            CollectionName = "cities";
        }

        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public string State { get; set; }
        [FirestoreProperty]
        public string Country { get; set; }
        [FirestoreProperty]
        public int PopSize { get; set; }
        [FirestoreProperty]
        public DateTime FoundedDt { get; set; }
    }
}