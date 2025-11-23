using System.ComponentModel.DataAnnotations;

namespace Alarm.Core.Models
{
    public class AlarmMode
    {
        [Key]
        public string Name { get; set; }
    }
}
