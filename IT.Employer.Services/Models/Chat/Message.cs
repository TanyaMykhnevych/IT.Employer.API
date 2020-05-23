using System;
using System.ComponentModel.DataAnnotations;

namespace IT.Employer.Services.Models.Chat
{
    public class Message
    {
        public Guid Id { get; set; }
        public string SenderName { get; set; }
        public Guid SenderId { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime When { get; set; }
        public Guid ReceiverId { get; set; }
        public string Type { get; set; }
    }
}
