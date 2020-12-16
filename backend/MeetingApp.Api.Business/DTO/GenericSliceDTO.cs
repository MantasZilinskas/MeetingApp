using System.Collections.Generic;

namespace MeetingApp.Api.Business.DTO
{
    public class GenericSliceDto<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
