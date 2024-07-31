// ReSharper disable UnusedType.Global - Implicit use through assembly scanning
using Mapster;
using MimeKit;

namespace LRSchoolV2.Email.Models.Actors;

public class EmailActorMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<EmailActor, MailboxAddress>
            .NewConfig()
            .MapWith(inEmailActor => new MailboxAddress(inEmailActor.Name, inEmailActor.Address));
    }
}