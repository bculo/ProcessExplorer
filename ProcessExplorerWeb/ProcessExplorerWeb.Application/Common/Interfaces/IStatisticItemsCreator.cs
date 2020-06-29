using ProcessExplorerWeb.Application.Common.Dtos.Stats;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Common.Interfaces
{
    public interface IStatisticItemsCreator
    {
        List<SingleStatisticItemDto> Items { get; }

        void AddItem(string title, string value);
        void AddItem(string title, DateTime date);
        void AddItem(string title, int value);
    }
}
