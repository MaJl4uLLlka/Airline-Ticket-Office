namespace AirLineTicketOffice.othersPatterns
{
    public interface IObserver
    {
        void Update(ISubject subject);
    }

    public interface ISubject
    {
        void Attach(IObserver subject);
        void Detach(IObserver subject);
        void Notify();
    }
}