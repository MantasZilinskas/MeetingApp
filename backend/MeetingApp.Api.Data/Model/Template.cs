using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace MeetingApp.Api.Data.Model
{
    public class Template
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EditorText { get; set; }
    }
}
