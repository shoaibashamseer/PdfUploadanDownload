using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;


namespace PdfUPload.Models
{
        public partial class PdfStore
        {
           
            public int Id { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public byte[] Attachment { get; set; } 
        }
    }

