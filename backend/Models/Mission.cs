using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Models
{
    public class Mission
    {
        public int Id { get; set; }
        
        public string Guid { get; set; }
        
        public string Name { get; set; }
        
        public string Url { get; set; }
        
        public int RobotId { get; set; }
        
        public Robot Robot { get; set; }
    }
}