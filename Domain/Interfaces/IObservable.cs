

using Domain.Models;

namespace Domain.Interfaces
{
    internal interface IObservable
    {
        void Attach(Driver observer);
        void Detach();
        void Notify();
        void NotifyAboutDelete();
    }
}
